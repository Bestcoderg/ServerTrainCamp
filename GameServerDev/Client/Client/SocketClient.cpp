#include "../include/SocketClient.h"
#include "../protocal/MsgID.pb.h"
#include "../include/PlayerMgr.h"
#include "../include/EventSystem.h"
#include "../protocal/GameSpec.pb.h"


SocketClient::SocketClient()
{
}

SocketClient::~SocketClient()
{
}

bool SocketClient::Init()
{
    return true;
}

void SocketClient::Uinit()
{
    m_epoll.Uinit();
}

bool SocketClient::OpenSocket(INT32 port)
{
    if(m_epoll.Init() == false)
    {
		std::cout << "Epoll add ev failed!" << std::endl;
		return false;
    }
    // listen socket init
    baselink* m_ListenSock = new baselink();
    if (m_ListenSock->Init(-1) == false)
    {
        return false;
    }
    std::cout << "port = " << port << std::endl;

    if (m_ListenSock->OpenClient() == -1)
    {
        std::cout << "Open Client failed!" << std::endl;
        return false;
    }
    m_ListenSock->ConnectServer(3000,"10.0.150.52");
    m_basefd = m_ListenSock->GetFD();
    //epoll
    m_epoll.EpollAdd(m_ListenSock);
    m_epoll.EpollAdd(STDIN_FILENO);
    return true;
}

void SocketClient::Dojob(INT32 port,std::string name,std::string passwd)
{
    if(!OpenSocket(port))
    {
        LOGERROR("Server open failed!");
        return ;
    }
    std::cout << "Server start!" << std::endl;
    while (true)
    {
        INT32 ready_size = m_epoll.EpollWait();
        if(ready_size < 0){
            std::cout << "ready_size < 0 " << std::endl;
            break;
        }
        for (int i = 0; i < ready_size; i++)
		{
            struct epoll_event* t_epev = m_epoll.GetEvent(i);
            INT32 rdyfd = t_epev->data.fd;
            if (t_epev->data.fd == STDIN_FILENO)   //键盘输入
			{
                // define buffer (static)
                static char head_buffer[HEADBUFFER_SIZE+1];   //pack head
                static char msg_buffer[BUFFER_SIZE+1];   //pack body == msg

                // get input
                std::cin.getline(msg_buffer,BUFFER_SIZE);
                int len = strlen(msg_buffer);
                if (len == 0)
                {
                    continue;
                }
                msg_buffer[len] = '\0';
                
                if(m_register)
                {
                    // send msgtest

                    MsgTest t_msg;
                    t_msg.set_id(1005);
                    t_msg.set_uid(123);
                    t_msg.set_msg(msg_buffer);
                    INT32 t_msg_len = t_msg.ByteSizeLong();
                    t_msg.SerializeToArray( msg_buffer, t_msg_len );
                    
                    MesgInfo t_msghed;
                    t_msghed.msgID = 1005;
                    t_msghed.uID = m_uid;
                    t_msghed.packLen = t_msg.ByteSizeLong();

                    m_epoll.SendMsg(t_msghed, t_msg, m_gatefd); 


                }
                else{
                    GameSpec::CtlMsgLoginReq t_msg;
                    t_msg.set_name(name);
                    t_msg.set_password(passwd);
                    std::cout << "name " << t_msg.name() << std::endl;
                    std::cout << "passwd " << t_msg.password() << std::endl;

                    MesgInfo t_msghed;
                    t_msghed.msgID = 9999;
                    t_msghed.uID = 0;
                    t_msghed.packLen = t_msg.ByteSizeLong();

                    m_epoll.SendMsg(t_msghed, t_msg, m_basefd); 

                }
			}
            else if (rdyfd == m_basefd) 
			{
                baselink* t_linker = m_epoll.GetLinkerByfd(rdyfd); 
                if (t_linker == 0)
                {
                    std::cout << "Can't find this sockfd in m_linmap , m_basefd = " << m_basefd << std::endl;
                }
                if(t_linker->RecvData() <= 0)
                {
                    continue;
                }


                INT32 t_packlen = t_linker->GetPackLens();
                while ( t_packlen != -1 )
                {
                    MesgInfo t_msginfo = t_linker->GetMsginfo();

                    char *str = t_linker->GetPack(t_packlen);
                    
                    if(t_msginfo.msgID == MSGID::MSG_TEST_ID)
                    {
                        MsgTest t_msg;
                        t_msg.ParseFromArray( str, t_msginfo.packLen );
                        std::cout << "protobuf receive: --------------------------" << std::endl;
                        std::cout << "msgid: " << t_msg.id() << std::endl;
                        std::cout << "uid: " << t_msg.uid() << std::endl;
                        std::cout << "msg: " << t_msg.msg() << std::endl;
                        std::cout << "protobuf receive: --------------------------" << std::endl;
                    }
                    else
                    {
                        GameSpec::CtlMsgLoginRsp test ;
                        test.ParseFromArray(str, t_msginfo.packLen);
                        if(test.errcode() == GameSpec::ERROR_NO_ERROR)
                        {
                            std::cout << "protobuf receive: --------------------------" << std::endl;
                            std::cout << "errorcode  = " << test.errcode() << std::endl;
                            std::cout << "ip  = " << test.ip() << std::endl;
                            std::cout << "port  = " << test.port() << std::endl;
                            std::cout << "uid  = " << test.uid() << std::endl;
                            std::cout << "protobuf receive: --------------------------" << std::endl;
			                
                            baselink* gatelinker = new baselink();
                            gatelinker->Init(-1);
                            gatelinker->OpenClient();
                            if(gatelinker->ConnectServer(test.port(), test.ip().c_str()) == -1)
                            {
                                std::cout << "Connect to Gate Server failed!" << std::endl;
                            }
                            std::cout << "Connect to Gate Server success!" << std::endl;
                            m_epoll.EpollAdd(gatelinker);
                            m_gatefd = gatelinker->GetFD();
                            m_uid = test.uid();
                            m_register = true;
                        }
                    }
                    t_packlen = t_linker->GetPackLens();
                }
            }
            else
			{
                baselink* t_linker = m_epoll.GetLinkerByfd(rdyfd); 
                if (t_linker == 0)
                {
                    std::cout << "Can't find this sockfd in m_linmap , m_basefd = " << m_basefd << std::endl;
                }
                if(t_linker->RecvData() <= 0)
                {
                    continue;
                }


                INT32 t_packlen = t_linker->GetPackLens();
                while ( t_packlen != -1 )
                {
                    MesgInfo t_msginfo = t_linker->GetMsginfo();

                    char *str = t_linker->GetPack(t_packlen);
                    
                    if(t_msginfo.msgID == MSGID::MSG_TEST_ID)
                    {
                        MsgTest t_msg;
                        t_msg.ParseFromArray( str, t_msginfo.packLen );
                        std::cout << "protobuf receive: --------------------------" << std::endl;
                        std::cout << "msgid: " << t_msginfo.msgID << std::endl;
                        std::cout << "uid: " << t_msginfo.uID << std::endl;
                        std::cout << "msg: " << t_msg.msg() << std::endl;
                        std::cout << "protobuf receive: --------------------------" << std::endl;
                    }
                    else
                    {
                        GameSpec::CtlMsgLoginRsp test ;
                        test.ParseFromArray(str, t_msginfo.packLen);
                        if(test.errcode() == GameSpec::ERROR_NO_ERROR)
                        {
                            std::cout << "protobuf receive: --------------------------" << std::endl;
                            std::cout << "errorcode  = " << test.errcode() << std::endl;
                            std::cout << "ip  = " << test.ip() << std::endl;
                            std::cout << "port  = " << test.port() << std::endl;
                            std::cout << "uid  = " << test.uid() << std::endl;
                            std::cout << "protobuf receive: --------------------------" << std::endl;
			                
                            baselink* gatelinker = new baselink();
                            gatelinker->Init(-1);
                            gatelinker->OpenClient();
                            if(gatelinker->ConnectServer(test.port(), test.ip().c_str()) == -1)
                            {
                                std::cout << "Connect to Gate Server failed!" << std::endl;
                            }
                            std::cout << "Connect to Gate Server success!" << std::endl;
                            m_epoll.EpollAdd(gatelinker);
                            m_gatefd = gatelinker->GetFD();
                            m_uid = test.uid();
                            m_register = true;
                        }
                    }
                    t_packlen = t_linker->GetPackLens();
                }


			}
		}
    }
}


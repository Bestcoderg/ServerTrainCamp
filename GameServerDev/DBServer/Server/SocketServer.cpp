#include "../include/SocketServer.h"
#include "../protocal/MsgID.pb.h"
#include "../include/EventSystem.h"
#include "../protocal/GameSpec.pb.h"

INSTANCE_SINGLETON(SocketServer);

SocketServer::SocketServer()
{
}

SocketServer::~SocketServer()
{
}

bool SocketServer::Init()
{
    return true;
}

void SocketServer::Uinit()
{
    m_epoll.Uinit();
}

bool SocketServer::OpenSocket(INT32 port)
{ 
    if(m_epoll.Init() == false)
    {
		std::cout << "Epoll add ev failed!" << std::endl;
		return false;
    }
    // base socket init
    baselink* m_ListenSock = new baselink();
    if (m_ListenSock->Init(-1) == false)
    {
        return false;
    }
    if (m_ListenSock->OpenServer(port) == -1)
    {
        std::cout << "Open server failed!" << std::endl;
        return false;
    }
    m_basefd = m_ListenSock->GetFD();
    m_epoll.EpollAdd(m_ListenSock);
    m_epoll.RegisterType(m_basefd, GameSpec::TYPE_DBSERVER);
    return true;
}

void SocketServer::Dojob(INT32 port)
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
			if (rdyfd == m_basefd) //新的链接，将新的socket加入epfd
			{
                int connfd = m_epoll.GetLinkerByfd(m_basefd)->AcceptSocket();
                if (connfd == -1)
                {
                    std::cout <<"Accept socket failed ! connfd == -1" <<std::endl;
                    continue;
                }
                if(m_epoll.EpollAdd(connfd) == -1)
                {
                    std::cout << "Epoll add connfd failed!" << connfd << std::endl;
                    continue;
                }
                std::cout << "New scoket has linked! sockfd = " << connfd << std::endl;
			}
			else
			{

                baselink* t_linker = m_epoll.GetLinkerByfd(rdyfd);
                if (t_linker == nullptr)
                {
                    std::cout << "Can't find client socket in linkmap, connfd = "<< rdyfd << std::endl;
                    continue;
                }
                INT32 ret = t_linker->RecvData();
                if (ret == 0)
                {
                    continue;
                }
                else if (ret == -1)
                {
                    m_epoll.EpollRemove(rdyfd);
                    // 前面已经确认过connfd一定存在
                    continue;
                }

                INT32 t_packlen = t_linker->GetPackLens();
                while ( t_packlen != -1 )
                {
                    //获得一整个包，注意这里才改变buffer的头指针
                    char *str = t_linker->GetPack(t_packlen);
                    //判断第一个包头的信息
                    const MesgInfo t_msginfo =t_linker->GetMsginfo();
                    std::cout << "t_mgsinfo  msgid = "<<t_msginfo.msgID << std::endl;
                    std::cout << "t_mgsinfo  uid = "<<t_msginfo.uID << std::endl;
                    std::cout << "t_mgsinfo  packlen = "<<t_msginfo.packLen << std::endl;
                    if(t_msginfo.msgID == MSGID::MSG_TEST_ID)
                    {
                        MsgTest t_msg;
                        t_msg.ParseFromArray( str, t_msginfo.packLen );
                        std::cout << "protobuf receive: --------------------------" << std::endl;
                        std::cout << "msgid: " << t_msg.id() << std::endl;
                        std::cout << "uid: " << t_msg.uid() << std::endl;
                        std::cout << "msg: " << t_msg.msg() << std::endl;
                        std::cout << "protobuf receive: --------------------------" << std::endl;
                        m_epoll.SendMsg(t_msginfo, t_msg, rdyfd);
                    }
                    else
                    {
                        //获得msgID绑定的函数，然后来处理对应的MSG
                        auto func = EventSystem::Instance()->GetMsgHandler()->GetMsgFunc(t_msginfo.msgID);
                        if (NULL != func)
                        {
                            //调用处理函数
                            (EventSystem::Instance()->*func)(t_msginfo, str, t_msginfo.packLen, rdyfd);
                        }
                        else
                        {
                            LOGINFO("Can't find function to execute Msg!  MSGID = "+std::to_string(t_msginfo.msgID));
                        }
                    }
                    t_packlen = t_linker->GetPackLens();
                }
			}
		}
    }
}


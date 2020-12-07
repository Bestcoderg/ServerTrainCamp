#include "../include/SocketServer.h"
#include "../protocal/MsgTest.pb.h"
#include "../include/PlayerMgr.h"
#include "../include/EventSystem.h"

INSTANCE_SINGLETON(SocketServer);

SocketServer::SocketServer()
{
    this->m_ListenSock = new baselink();
    this->m_msg_head = new MesgHead();
}

SocketServer::~SocketServer()
{
    delete m_msg_head;
    delete m_ListenSock;
}


bool SocketServer::Init()
{
    m_linkmap.clear();
    // base socket init
    if (m_ListenSock->Init(-1) == false)
    {
        return false;
    }
    if (m_ListenSock->OpenServer(DEFAULT_SERVER_PORT, DEFAULT_SERVER_ADDR) == -1)
    {
        std::cout << "Open server failed!" << std::endl;
        return false;
    }
    m_basefd = m_ListenSock->GetFD();

    // epoll init 
    m_Epfd = epoll_create(MAX_LINK_COUNT);
    if(m_Epfd == -1){
        return false;
    }
    ev.events = EPOLLIN;
	ev.data.fd = m_ListenSock->GetFD();
	if (epoll_ctl(m_Epfd, EPOLL_CTL_ADD, m_basefd, &ev) == -1)
	{
		std::cout << "Epoll add ev failed!" << std::endl;
		return false;
	}
    return true;
}

void SocketServer::Uinit()
{
    m_msg_head->Uinit();
    //在使用clear（）的时候，对象副本会去走到析构函数，进行对象内部的内存释放
    //stl 牛逼
    m_linkmap.clear();
    m_ListenSock->Uinit();
}

void SocketServer::Dojob()
{
    std::cout << "Server start!" << std::endl;
    while (true)
    {
        INT32 ready_size = epoll_wait(m_Epfd, events, MAX_LINK_COUNT, -1);
        if(ready_size < 0){
            std::cout << "ready_size < 0 " << std::endl;
            break;
        }
        for (int i = 0; i < ready_size; i++)
		{
			if (events[i].data.fd == m_basefd) //新的链接，将新的socket加入epfd
			{
                int connfd = m_ListenSock->AcceptSocket();
                if (connfd == -1)
                {
                    std::cout <<"connfd == -1" <<std::endl;
                    continue;
                }
                m_linkmap[connfd] = new baselink();
                m_linkmap[connfd]->Init(connfd);
                
                ev.events = EPOLLIN;
                ev.data.fd = connfd;
				epoll_ctl(m_Epfd, EPOLL_CTL_ADD, connfd, &ev);
                std::cout << "New client has linked! sockfd = " << connfd << std::endl;
			}
			else
			{
				int connfd = events[i].data.fd;
                baselink*& t_linker = m_linkmap[connfd];
                if (t_linker == 0)
                {
                    std::cout << "Can't find client socket in linkmap, connfd = "<< connfd << std::endl;
                    continue;
                }
                INT32 ret = t_linker->RecvData();           
                if (ret == 0)
                {
                    continue;
                }
                else if (ret == -1)
                {
                    // 前面已经确认过connfd一定存在
                    m_linkmap.erase(m_linkmap.find(connfd));
                    continue;
                }



                //send package
                INT32 t_buf_size = t_linker->GetBufferSize();
                INT32 t_msghead_size = m_msg_head->GetMsgHeadSize();
                while ( t_msghead_size <= t_buf_size )
                {
                    MesgInfo t_msginfo = m_msg_head->decode(t_linker->GetBufferHead());
                    // std::cout << "t_msginfo.packlen = " << t_msginfo.packLen  << std::endl;
                    INT32 t_pack_len = t_msginfo.packLen + t_msghead_size;
                    if (t_pack_len > t_buf_size)
                    {
                        std::cout << "Pack have not been reveived completed, Pack'len = " << t_pack_len << " ,  buffer len = " << t_buf_size << std::endl;
                        break;
                    }

                    char *str = t_linker->GetPack(t_pack_len);
                    MsgTest t_msg;
                    t_msg.ParseFromArray( &str[t_msghead_size], t_msginfo.packLen );
                    std::cout << "protobuf receive: --------------------------" << std::endl;
                    std::cout << "msgid: " << t_msg.id() << std::endl;
                    std::cout << "uid: " << t_msg.uid() << std::endl;
                    std::cout << "msg: " << t_msg.msg() << std::endl;
                    std::cout << "protobuf receive: --------------------------" << std::endl;
                    
                    //test
                    //PlayerMgr::Instance()->RegisterPlayer(t_msg.uid());
                    auto func = EventSystem::Instance()->GetMsgHandler()->GetMsgFunc(t_msg.id());
                    if (NULL != func)
                    {
                        (EventSystem::Instance()->*func)(t_msginfo, &str[t_msghead_size], t_msginfo.packLen);
                        //(PlayerMgr::Instance()->GetPlayerByID(t_msg.uid())->*func)(t_msginfo, &str[t_msghead_size], t_msginfo.packLen);
                    }

                    //test


                    t_buf_size = t_linker->GetBufferSize();
                    t_msghead_size = m_msg_head->GetMsgHeadSize();
                    
                    INT32 buf_size = t_pack_len;
                    //char *str = m_linkmap[connfd]->GetBuffer(buf_size);
                    for (auto it = m_linkmap.begin(); it != m_linkmap.end(); it++)
                    {
                        it->second->SendData(str, buf_size);
                    }


                }
			}
		}
    }
}

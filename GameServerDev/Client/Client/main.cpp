#include "../include/SocketClient.h"
int main()
{   
    int port;
    std::cout << "port: ";
    std::cin>>port;
    std::string name,passwd;
    std::cout << "please input your name: ";
    std::cin >> name;
    std::cout << "please input your passwd: ";
    std::cin >> passwd;

    SocketClient* client = new SocketClient();
    client->Dojob(port,name,passwd);

    return 0;
}

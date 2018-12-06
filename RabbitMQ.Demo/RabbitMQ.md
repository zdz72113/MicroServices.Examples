[TOC]
# RabbitMQ简介
AMQP，即Advanced Message Queuing Protocol，高级消息队列协议，是应用层协议的一个开放标准，为面向消息的中间件设计。消息中间件主要用于组件之间的解耦，消息的发送者无需知道消息使用者的存在，反之亦然。

AMQP的主要特征是面向消息、队列、路由（包括点对点和发布/订阅）、可靠性、安全。
RabbitMQ是一个开源的AMQP实现，服务器端用Erlang语言编写，支持多种客户端，如：Python、Ruby、.NET、Java、JMS、C、PHP、ActionScript、XMPP、STOMP等，支持AJAX。用于在分布式系统中存储转发消息，在易用性、扩展性、高可用性等方面表现不俗。

RabbitMQ提供了可靠的消息机制、跟踪机制和灵活的消息路由，支持消息集群和分布式部署。适用于排队算法、秒杀活动、消息分发、异步处理、数据同步、处理耗时任务、CQRS等应用场景。

# RabbitMQ原理简介
![RabbitMQ diagram](https://images2015.cnblogs.com/blog/42248/201604/42248-20160417100716723-1488755019.png)

RabbitMQ中间件分为服务端（RabbitMQ Server）和客户端（RabbitMQ Client），服务端可以理解为是一个消息的代理消费者，客户端又分为消息生产者（Producer）和消息消费者（Consumer）。

　　1. 消息生产者（Producer）：主要生产消息并将消息基于TCP协议，通过建立Connection和Channel，将消息传输给RabbitMQ Server，对于Producer而言基本就完成了工作。

　　2. 服务端（RabbitMQ Server）：主要负责处理消息路由、分发、入队列、缓存和出列。主要由三部分组成：Exchange、RoutingKey、Queue。

- Exchange：用于接收消息生产者发送的消息，有三种类型的exchange：direct, fanout,topic，不同类型实现了不同的路由算法；

    - direct exchange：将与routing key 比配的消息，直接推入相对应的队列，创建队列时，默认就创建同名的routing key。

    - fanout exchange：是一种广播模式，忽略routingkey的规则。

    - topic exchange：应用主题，根据key进行模式匹配路由，例如：若为abc*则推入到所有abc*相对应的queue；若为abc.#则推入到abc.xx.one ,abc.yy.two对应的queue。

- RoutingKey：是RabbitMQ实现路由分发到各个队列的规则，并结合Binging提供于Exchange使用将消息推送入队列；
- Queue：是消息队列，可以根据需要定义多个队列，设置队列的属性，比如：消息移除、消息缓存、回调机制等设置，实现与Consumer通信；

3. 消息消费者（Consumer）：主要负责消费Queue的消息，同样基于TCP协议，通过建立Connection和Channel与Queue传输消息，一个消息可以给多个Consumer消费；

4. 关键名词说明：Connection、Channel、Binging等；

- Connection：是建立客户端与服务端的连接。

- Channel：是基于Connection之上建立通信通道，因为每次Connection建立TCP协议通信开销及性能消耗较大，所以一次建立Connection后，使用多个Channel通道通信减少开销和提高性能。

- Binging：是一个捆绑定义，将exchange和queue捆绑，定义routingkey相关策略。

# RabbitMQ安装
因为RabbitMQ由Erlang实现，本机部署的话还要首先安装Erlang的开发环境。然而借助Docker的话，环境部署便会非常便捷。这里来使用docker快速搭建带web管理功能的RabbitMQ的环境。

[Docker官方镜像地址](https://hub.docker.com/r/library/rabbitmq/)
1. 查找镜像

```
#查找tag为management的rabbitmq镜像
docker search rabbitmq:management
```

2. 获取镜像

```
docker pull rabbitmq:management
```
> 如果docker pull rabbitmq 后面不带management，启动rabbitmq后是无法打开管理界面的.

3. 运行镜像

```
docker run -d --name rabbitmq -p 5671:5671 -p 5672:5672 -p 4369:4369 -p 25672:25672 -p 15671:15671 -p 15672:15672 rabbitmq:management
```
> 管理界面的默认端口是15672。同时默认创建了一个guest 用户，密码也是guest。

# .NET Core 使用 RabbitMQ
## Hello World
生产者（producer）把消息发送到一个名为“hello”的队列中。消费者（consumer）从这个队列中获取消息。
![hello_world](http://www.rabbitmq.com/img/tutorials/python-one.png)

## 工作队列
工作队列是为了避免等待一些占用大量资源或时间的操作。当我们把任务当作消息发送到队列中，一个或多个工作者会取出任务轮询处理。
![work_queue](http://www.rabbitmq.com/img/tutorials/python-two.png)

## 扇型交换机
扇型交换机不需要设置路由键，它会把消息发送给绑定在它上面的所有队列。类似于广播。

![fanout](http://www.rabbitmq.com/img/tutorials/python-three-overall.png)

## 直连交换机
直连交换机对绑定键和路由键进行精确匹配，从而确定消息该分发到哪个队列。相比于扇形交换机盲目的广播消息，直连交换改进为可以选择性的接收。
![direct](http://www.rabbitmq.com/img/tutorials/python-four.png)

## 主题交换机
主题交换机的路由键必须是一个由.分隔开的词语列表。绑定键拥有相同的格式，但可以用*表示一个单词或者用# 用来表示任意数量（零个或多个）的单词。通过模式匹配可以基于多个标准执行路由操作。
> 主题交换机是很强大的，它可以表现出跟其他交换机类似的行为
> 
> 当一个队列的绑定键为 "#"（井号） 
> 的时候，这个队列将会无视消息的路由键，接收所有的消息。
> 
> 当 * (星号) 和 # (井号) 这两个特殊字符都未在绑定键中出现的时候，此时主题交换机就拥有的直连交换机的行为。

![topic](http://www.rabbitmq.com/img/tutorials/python-five.png)

## 远程过程调用
RPC工作原理如下：
1. 当客户端启动的时候，它创建一个匿名独享的回调队列。
2. 在RPC请求中，客户端发送带有两个属性的消息：一个是设置回调队列的 reply_to 属性，另一个是设置唯一值的 correlation_id 属性。
3. 将请求发送到一个 rpc_queue 队列中。
4. RPC工作者（又名：服务器）等待请求发送到这个队列中来。当请求出现的时候，它执行他的工作并且将带有执行结果的消息发送给reply_to字段指定的队列。
5. 客户端等待回调队列里的数据。当有消息出现的时候，它会检查correlation_id属性。如果此属性的值与请求匹配，将它返回给应用。
![rpc](http://www.rabbitmq.com/img/tutorials/python-six.png)

# 扩展组件

## MassTransit

MassTransit 是一个自由、开源、轻量级的消息总线, 用于使用. NET 框架创建分布式应用程序。MassTransit 在现有消息传输上提供了一组广泛的功能, 从而使开发人员能够友好地使用基于消息的会话模式异步连接服务。基于消息的通信是实现面向服务的体系结构的可靠和可扩展的方式。

类似的国外组件还有NServiceBus, 是收费的，据说MassTransit比NServiceBus更加轻量级，并且在开发之初就选用了RabbitMQ作为消息传输组件，MassTransit还支持Azure Service Bus。

[官网地址](http://masstransit-project.com/) [GitHub地址](https://github.com/MassTransit/MassTransit) 

## EasyNetQ

EasyNetQ是一款基于RabbitMQ.Client封装的API库，正如其名，使用起来比较Easy，它把原RabbitMQ.Client中的很多操作都进行了再次封装，让开发人员减少了很多工作量。

[官网地址](http://easynetq.com) [GitHub地址](https://github.com/EasyNetQ/EasyNetQ) 

# RabbitMQ与Kafka

RabbitMQ与Kafka的对比，可参阅这篇文章：[开源软件成熟度评测报告-分布式消息中间件](https://blog.csdn.net/yssycz/article/details/80133084)

# 示例代码
[github](https://github.com/zdz72113/MicroServices.Examples/tree/master/RabbitMQ.Demo)

# 参考
- [.NET Core 使用RabbitMQ](https://www.cnblogs.com/stulzq/p/7551819.html)
- [部署带Web管理工具的RabbitMQ](https://blog.csdn.net/mungo/article/details/78663432)
- [RabbitMQ消息队列应用](http://www.cnblogs.com/Andon_liu/p/5401961.html)
- [RabbitMQ 中文文档](http://rabbitmq.mr-ping.com/)
- [EasyNetQ](https://github.com/EasyNetQ/EasyNetQ/wiki)
- [MassTransit](https://github.com/MassTransit/MassTransit/blob/develop/docs/usage/README.md)
- [.NET Core微服务之基于MassTransit实现数据最终一致性](https://www.cnblogs.com/edisonchou/p/dnc_microservice_masstransit_foundation_part1.html)
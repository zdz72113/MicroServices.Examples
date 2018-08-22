
- [OpenID 和 OAuth 的区别](#OpenIDvsOAuth)
- [IdentityServer4，NET Core下的安全框架](#identityServer4)
- [客户端模式（Client Credentials）](#client)
- [密码模式（resource owner password credentials）](#password)
- [简化模式（implicit flow）](#implicit)
- [授权码模式（Authorization code flow）](#code)
- [混合模式（Hybrid Flow）](#hybird)

<span id="OpenIDvsOAuth"></span>
#### OpenID 和 OAuth 的区别
- OpenID：Authentication，即认证，用户是谁？
- OAuth：Authorization，即授权，用户能做哪些操作？
- OpenID Connect（OIDC）：基于OAuth协议，是“认证”和“授权”的结合。
    > OAuth2提供了Access Token来解决授权第三方客户端访问受保护资源的问题。 OIDC在这个基础上提供了ID Token来解决第三方客户端标识用户身份认证的问题。

<span id="identityServer4"></span>
#### IdentityServer4
[IdentityServer4](https://identityserver.io/) 是 ASP.NET Core的一个包含OpenID Connect和OAuth 2.0协议的框架，提供了单点登录，集中控制，API访问控制等功能。

<span id="client"></span>
#### 客户端模式（Client Credentials）
适用于和用户无关，机器与机器之间直接交互访问资源的场景。

```
POST https://api.oauth2server.com/token
    grant_type=client_credentials&
    client_id=CLIENT_ID&
    client_secret=CLIENT_SECRET
```

<span id="password"></span>
#### 密码模式（resource owner password credentials）
适用于当前的APP是专门为服务端设计的情况。

```
POST https://api.oauth2server.com/token
  grant_type=password&
  username=USERNAME&
  password=PASSWORD&
  client_id=CLIENT_ID
```

<span id="implicit"></span>
#### 简化模式（implicit flow）
适用于浏览器WEB应用，支持
- 用户认证（JavaScript 应用或传统服务端渲染的Web应用）
- 用户认证+授权（JavaScript应用）

简化模式下ID Token和Access Token都是通过浏览器的前端通道传递的。

所以如果是传统服务端Web应用并且仅是在服务端使用Access Token的话，推荐使用Hybrid Flow。
![OpenID Implicit flow](https://note.youdao.com/yws/public/resource/376e602a65294dc2068248dbe41b9641/xmlnote/418D1E0E4F394D679918E6E58DE9A872/5691)

<span id="code"></span>
#### 授权码模式（Authorization code flow）
授权码模式通过后台传输Tokens，相对于简化模式会更安全一点。

但每当考虑使用授权码模式的时候，请使用混合模式。混合模式会首先返回一个可验证的ID Token并且有更多其他特性。

![Authorization code flow](https://note.youdao.com/yws/public/resource/376e602a65294dc2068248dbe41b9641/xmlnote/7125C06FB6D74830BC903425CD8D8045/5693)

<span id="hybrid"></span>
#### 混合模式（Hybrid flow）
适用于服务器端 Web 应用程序和原生桌面/移动应用程序。

混合模式是简化模式和授权码模式的组合。混合模式下ID Token通过浏览器的前端通道传递，而Access Token和Refresh Token通过后端通道取得。

#### 参考
- [认证与授权——单点登录协议盘点：OpenID vs OAuth2 vs SAML](https://www.jianshu.com/p/5d535eee0a9b)
- [IdentityServer4 实现 OpenID Connect 和 OAuth 2.0](http://www.cnblogs.com/xishuai/p/identityserver4-implement-openid-connect-and-oauth2.html)
- [blackheart-OIDC](http://www.cnblogs.com/linianhui/tag/OIDC/)
- [Which OpenID Connect/OAuth 2.0 Flow is the right One?](https://leastprivilege.com/2016/01/17/which-openid-connectoauth-2-o-flow-is-the-right-one/)

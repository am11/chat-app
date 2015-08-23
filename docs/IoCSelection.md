# IoC Framework Selection

There are many frameworks for inversion of control. Many have overlapping features. The distinguishing features are mostly concerned with obscure scenarios such as complex generics and reflection.

I investigated AutoFac, Ninject and Simple Injector and chosen Simple Injector as it covers majority of vastly used scenarios, while keeping the concerns separated. Each type of project has a separate method of configuration, crafted per the conventions of the project system (such as Web Forms, MVC, API etc.). 

Although AutoFac and Ninject have overlapping features, I found simple injector quite flexible as advanced configuration can be made in-place without tuning so many knobs. For instance, there is a simple service locator for Azure table entity injected in [SignalR's startup class](https://github.com/am11/chat-app/blob/master/src/jutteleminen-sovellus/Hubs/StartUp.cs) without much noise. Similarly, in [Global.asax.cs](https://github.com/am11/chat-app/blob/master/src/jutteleminen-sovellus/Global.asax.cs), inversion of control via constructor injection is configured for MVC Controllers, which is consumed in the HomeController. 

With that said, I used Ninject long time ago in .NET 3.0 WCF project and found it very handy at the time. Since then I used native dependency injection approaches in .NET, MEF (which is not a true DI container anyway but is excellent way of inverting the control in on-demand, modular way) and tried a little tutorial on state machine driven service-selection in upcoming MVC6. Simple Injector is a lightweight integration and a useful framework and hasn't disappointed me to carry out the task in hand with lesser keystrokes! :)

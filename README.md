Tabasco
=======
Tabasco is a simple Web framework that drops a little spice on top of NRack.

Example
---------

```c#
public class App : TabascoBase
{
  [Get]
  public IView Index()
  {
      return new Spark();
  }

  [Post("/name")]
  public IView Name()
  {
      return new Razor(new { Name = Request.Params["name"] });
  }

  [Get("/doctor")]
  [Get("/doctor/:who")]
  public dynamic[] Pepper()
  {
      var who = Request.Params.ContainsKey(":who") ? Request.Params[":who"] : "you";

      return new dynamic[]
                 {
                   200, 
                   new Hash { { "Content-Type", "text/plain" } }, 
                   "Wouldn't " + who + " like to be a pepper, too?"
                 };
  }
}
```

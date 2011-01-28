Tabasco
=======
Tabasco is a simple Web framework that drops a little spice on top of NRack.

Example
---------
    [Resource("/")]
    public class Main
    {
      [Get]
      public string Root()
      {
        return "Hello, Tabasco!";
      }

      [Get("/doctor")]
      public dynamic[] Pepper()
      {
        return new dynamic[]
         {
           200, 
           new Hash { { "Content-Type", "text/plain" } }, 
           "Wouldn't you like to be a pepper, too?"
         };
      }
    }

Philosophy
----------
* URL's matter!
* Testing should be easy.
* The HTTP Pipeline should be open for middleware.


Todo
-------
* Stronger URL routing with named parameters.

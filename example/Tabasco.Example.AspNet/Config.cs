using System;
using System.IO;
using NRack;
using NRack.Configuration;
using NRack.Helpers;

namespace Tabasco.Example.AspNet
{
    public class Config : ConfigBase
    {
        public override void Start()
        {
            Use<Static>(new Hash
                            {
                                {"urls", new[] {"/styles"}}, 
                                {"root", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public")}
                            })
            .Run(new App());
        }
    }
}
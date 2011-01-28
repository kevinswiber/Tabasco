using NRack.Configuration;

namespace Tabasco.Example
{
    public class Config : ConfigBase
    {
        #region Overrides of ConfigBase

        public override void Start()
        {
            Run(new TabascoApplication());
        }

        #endregion
    }
}
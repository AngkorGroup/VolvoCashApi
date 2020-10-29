using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using VolvoCash.CrossCutting.Adapter;

namespace VolvoCash.CrossCutting.NetFramework.Adapter
{
    public class AutomapperTypeAdapterFactory : ITypeAdapterFactory
    {
        #region Members
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new Automapper type adapter factory
        /// </summary>
        public AutomapperTypeAdapterFactory()
        {
            //scan all assemblies finding Automapper Profile
            var profiles = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .Where(x => x.GetName().FullName.Contains("VolvoCash"))
                                    .SelectMany(a => a.GetTypes())
                                    .Where(t => t.GetTypeInfo().BaseType == typeof(Profile));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var item in profiles)
                {
                    if (item.FullName != "AutoMapper.SelfProfiler`2" &&
                        item.FullName != "AutoMapper.MapperConfiguration+NamedProfile")

                        cfg.AddProfile(Activator.CreateInstance(item) as Profile);
                }
            });
            _mapper = config.CreateMapper();
        }
        #endregion

        #region Public Methods
        public ITypeAdapter Create()
        {
            return new AutomapperTypeAdapter(_mapper);
        }
        #endregion
    }
}

using System;
using AutoMapper;
using VolvoCash.CrossCutting.Tests.Classes;

namespace VolvoCash.CrossCutting.Tests
{
    public class AutomapperInitializer : IDisposable
    {
        public IMapper _mapper;
        public AutomapperInitializer()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TypeAdapterProfile());
            });
            _mapper = config.CreateMapper();
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

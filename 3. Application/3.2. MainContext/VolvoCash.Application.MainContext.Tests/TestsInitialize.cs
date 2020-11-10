using System;
using Xunit;
using VolvoCash.Application.MainContext.DTO.CardTypes;
using VolvoCash.CrossCutting.Validator;
using VolvoCash.CrossCutting.NetFramework.Validator;
using VolvoCash.CrossCutting.NetFramework.Adapter;
using VolvoCash.CrossCutting.Adapter;
using VolvoCash.CrossCutting.NetFramework.Localization;
using VolvoCash.CrossCutting.Localization;

namespace VolvoCash.Application.MainContext.Tests
{
    public class TestsInitialize : IDisposable
    {
        public TestsInitialize()
        {
            InitializeFactories();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        private void InitializeFactories()
        {
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());

            var dto = new CardTypeDTO(); // this is only to force  current domain to load de .DTO assembly and all profiles
            var adapterfactory = new AutomapperTypeAdapterFactory();
            TypeAdapterFactory.SetCurrent(adapterfactory);

            //Localization
            LocalizationFactory.SetCurrent(new ResourcesManagerFactory());
        }
    }

    [CollectionDefinition("Our Test Collection #2")]
    public class Collection2 : ICollectionFixture<TestsInitialize>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}

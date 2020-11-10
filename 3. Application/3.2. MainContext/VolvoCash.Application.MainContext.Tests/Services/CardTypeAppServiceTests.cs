using Xunit;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Application.MainContext.CardTypes.Services;

namespace VolvoCash.Application.MainContext.Tests.Services
{
    [Collection("Our Test Collection #2")]
    public class CardTypeAppServiceTests
    {
        #region Members
        protected TestsInitialize fixture;
        protected ICardTypeAppService _cardTypeAppService;
        #endregion

        #region Constructor
        public CardTypeAppServiceTests(TestsInitialize fixture,
                                       ICardTypeAppService cardTypeAppService)
        {
            this.fixture = fixture;
            this._cardTypeAppService = cardTypeAppService;
        }
        #endregion

        #region Tests
        [Fact]
        public void AddCardTypeReturnDTOWhenSaveSucceed()
        {
            //Given
            var cardType = new CardType
            {
                Name = "VTEST",
                DisplayName = "Volvo Creado desde Unit Test",
                Currency = Currency.USD,
                Term = 54,
                Color = "#000000",
                TPCode = "1000",
                Status = Status.Active
            };

            //When
            var cardTypeResult = _cardTypeAppService.AddAsync(cardType);

            //Then
            Assert.NotNull(cardTypeResult);
        }
        #endregion
    }
}

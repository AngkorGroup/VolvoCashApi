using Newtonsoft.Json.Converters;
using VolvoCash.CrossCutting.Utils.Constants;

namespace VolvoCash.Application.Seedwork.DateConverters
{

    public class DefaultDateConverter : IsoDateTimeConverter
    {
        public DefaultDateConverter()
        {
            base.DateTimeFormat = DateTimeFormats.DateFormat;
        }
    }

    public class DefaultLiterallyDateConverter : IsoDateTimeConverter
    {
        public DefaultLiterallyDateConverter()
        {
            base.DateTimeFormat = DateTimeFormats.DateLiterallyFormat + " " + DateTimeFormats.TimeShortFormat;
        }
    }

    public class DefaultShortLiterallyDateConverter : IsoDateTimeConverter
    {
        public DefaultShortLiterallyDateConverter()
        {
            base.DateTimeFormat = DateTimeFormats.DateLiterallyFormat;
        }
    }

    public class DefaultDateTimeConverter : IsoDateTimeConverter
    {
        public DefaultDateTimeConverter()
        {
            base.DateTimeFormat = DateTimeFormats.DateFormat + " " + DateTimeFormats.TimeFormat;
        }
    }
}

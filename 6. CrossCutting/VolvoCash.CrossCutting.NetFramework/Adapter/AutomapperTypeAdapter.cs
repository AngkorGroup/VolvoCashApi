using AutoMapper;
using VolvoCash.CrossCutting.Adapter;

namespace VolvoCash.CrossCutting.NetFramework.Adapter
{
    /// <summary>
    /// Automapper type adapter implementation
    /// </summary>
    public class AutomapperTypeAdapter : ITypeAdapter
    {
        #region Members
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AutomapperTypeAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            return _mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Adapt<TTarget>(object source)
            where TTarget : class, new()
        {
            return _mapper.Map<TTarget>(source);
        }
        #endregion
    }
}

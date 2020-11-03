using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Domain.Seedwork;
using VolvoCash.Application.Seedwork.Common;

namespace VolvoCash.DistributedServices.Seedwork.Controllers
{
    [Route("api/[controller]")]
    public abstract class AsyncBaseApiController<TEntity, TEntityDTO> : Controller
        where TEntity : Entity
        where TEntityDTO : Entity
    {
        #region Members
        protected IService<TEntity, TEntityDTO> _service;
        #endregion

        #region Constructor
        protected AsyncBaseApiController(IService<TEntity, TEntityDTO> service)
        {
            _service = service;
        }
        #endregion

        #region Public Methods
        // GET api/Entity
        [HttpGet]
        public virtual async Task<IEnumerable<TEntityDTO>> Get()
        {
            return await _service.GetAllDTOAsync();
        }

        // GET api/Entity/id
        [HttpGet("{id}")]
        public virtual async Task<TEntityDTO> GetById(int id)
        {
            return await _service.GetDTOAsync(id);
        }

        // POST api/Entity
        [HttpPost]
        public virtual async Task<TEntityDTO> Post([FromBody] TEntityDTO entityDTO)
        {
            return await _service.AddAsync(entityDTO);
        }

        // POST api/Entity/PostItems
        [HttpPost("PostItems")]
        public virtual async Task<IEnumerable<TEntityDTO>> PostItems([FromBody] IEnumerable<TEntityDTO> entitiesDTO)
        {
            return await _service.AddAsync(entitiesDTO.ToList());
        }

        //// PUT api/Entity
        [HttpPut()]
        public virtual async Task<TEntityDTO> Put([FromBody] TEntityDTO entityDTO)
        {
            return await _service.ModifyAsync(entityDTO);
        }

        // PUT api/Entity/PutWithId/5
        [HttpPut("{id}")]
        public virtual async Task<TEntityDTO> PutWithId(int id, [FromBody] TEntityDTO entityDTO)
        {
            return await _service.ModifyAsync(id, entityDTO);
        }

        // PUT api/Entity/PutItems
        [HttpPut("PutItems")]
        public virtual async Task<IEnumerable<TEntityDTO>> PutItems([FromBody] IEnumerable<TEntityDTO> entitiesDTO)
        {
            return await _service.AddAsync(entitiesDTO.ToList());
        }

        // DELETE api/Entity/5
        [HttpDelete("{id}")]
        public virtual async Task Delete(int id)
        {
            await _service.RemoveAsync(id);
        }

        // DELETE api/Entity/DeleteItems
        [HttpDelete("DeleteItems")]
        public virtual async Task DeleteItems(IEnumerable<int> ids)
        {
            await _service.RemoveAsync(ids);
        }
        #endregion
    }
}

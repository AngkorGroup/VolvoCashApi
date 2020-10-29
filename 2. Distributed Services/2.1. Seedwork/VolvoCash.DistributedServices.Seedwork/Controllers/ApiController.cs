using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.Domain.Seedwork;
using VolvoCash.Application.Seedwork.Common;

namespace VolvoCash.DistributedServices.Seedwork.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseApiController<TEntity, TEntityDTO> : Controller
        where TEntity : Entity
        where TEntityDTO : Entity
    {
        #region Members
        protected IService<TEntity, TEntityDTO> _service;
        #endregion

        #region Constructor
        protected BaseApiController(IService<TEntity, TEntityDTO> service)
        {
            _service = service;
        }
        #endregion

        #region Public Methods
        // GET api/Entity
        [HttpGet]
        public virtual IEnumerable<TEntityDTO> Get()
        {
            return _service.GetAllDTO();
        }

        // GET api/Entity/GetById/id
        [HttpGet("GetById/{id}")]
        public virtual TEntityDTO GetById(int id)
        {
            return _service.GetDTO(id);
        }

        // POST api/Entity
        [HttpPost]
        public virtual TEntityDTO Post([FromBody] TEntityDTO entityDTO)
        {
            return _service.Add(entityDTO);
        }

        // POST api/Entity/PostItems
        [HttpPost("PostItems")]
        public virtual IEnumerable<TEntityDTO> PostItems([FromBody] IEnumerable<TEntityDTO> entitiesDTO)
        {
            return _service.Add(entitiesDTO.ToList());
        }

        // PUT api/Entity
        [HttpPut()]
        public virtual TEntityDTO Put([FromBody] TEntityDTO entityDTO)
        {
            return _service.Modify(entityDTO);
        }

        // PUT api/Entity/PutWithId/5
        [HttpPut("PutWithId/{id}")]
        public virtual TEntityDTO PutWithId(int id, [FromBody] TEntityDTO entityDTO)
        {
            return _service.Modify(id, entityDTO);
        }

        // PUT api/Entity/PutItems
        [HttpPut("PutItems")]
        public virtual IEnumerable<TEntityDTO> PutItems([FromBody] IEnumerable<TEntityDTO> entitiesDTO)
        {
            return _service.Add(entitiesDTO.ToList());
        }

        // DELETE api/Entity/5
        [HttpDelete("{id}")]
        public virtual void Delete(int id)
        {
            _service.Remove(id);
        }

        // DELETE api/Entity/DeleteItems
        [HttpDelete("DeleteItems")]
        public virtual void DeleteItems(IEnumerable<int> ids)
        {
            _service.Remove(ids);
        }
        #endregion
    }
}

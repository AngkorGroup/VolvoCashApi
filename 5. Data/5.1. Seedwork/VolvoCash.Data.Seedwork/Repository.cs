using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VolvoCash.Domain.Seedwork;
using VolvoCash.Domain.Seedwork.Specification;
using VolvoCash.CrossCutting.Localization;

namespace VolvoCash.Data.Seedwork
{
    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="TEntity">The type of underlying entity in this repository</typeparam>
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : Entity
        where TContext : class
    {
        #region Members
        IQueryableUnitOfWork _UnitOfWork;
        private readonly ILogger _logger;
        public TContext _context { get => _UnitOfWork as TContext; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        /// <param name="logger">Logger</param>
        public Repository(IQueryableUnitOfWork unitOfWork, ILogger<Repository<TEntity, TContext>> logger)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            _UnitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region Private Methods
        DbSet<TEntity> GetSet()
        {
            return _UnitOfWork.CreateSet<TEntity>();
        }
        #endregion

        #region Public Methods
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }

        public virtual void Add(TEntity item)
        {
            if (item != (TEntity)null)
            {
                GetSet().Add(item); // add new item in this set
            }
            else
            {
                _logger.LogInformation(LocalizationFactory.CreateLocalResources().GetStringResource(LocalizationKeys.Infraestructure.info_CannotAddNullEntity), typeof(TEntity).ToString());
            }
        }

        public virtual void Add(IEnumerable<TEntity> items)
        {
            if (items != null)
            {
                foreach (var item in items) this.Add(item);
            }
            else
            {
                _logger.LogInformation(LocalizationFactory.CreateLocalResources().GetStringResource(LocalizationKeys.Infraestructure.info_CannotAddNullEntity), typeof(TEntity).ToString());
            }
        }

        public virtual void Remove(TEntity item)
        {
            if (item != (TEntity)null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
            else
            {
                _logger.LogInformation(LocalizationFactory.CreateLocalResources().GetStringResource(LocalizationKeys.Infraestructure.info_CannotRemoveNullEntity), typeof(TEntity).ToString());
            }
        }

        public virtual void Remove(IEnumerable<TEntity> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    this.Remove(item);
                }
            }
            else
            {
                _logger.LogInformation(LocalizationFactory.CreateLocalResources().GetStringResource(LocalizationKeys.Infraestructure.info_CannotRemoveNullEntity), typeof(TEntity).ToString());
            }
        }

        public virtual void TrackItem(TEntity item)
        {
            if (item != (TEntity)null)
            {
                _UnitOfWork.Attach<TEntity>(item);
            }
            else
            {
                _logger.LogInformation(LocalizationFactory.CreateLocalResources().GetStringResource(LocalizationKeys.Infraestructure.info_CannotTrackNullEntity), typeof(TEntity).ToString());
            }
        }

        public virtual void TrackItem(IEnumerable<TEntity> items)
        {
            if (items != null)
            {
                foreach (var item in items) this.TrackItem(item);
            }
            else
            {
                _logger.LogInformation(LocalizationFactory.CreateLocalResources().GetStringResource(LocalizationKeys.Infraestructure.info_CannotTrackNullEntity), typeof(TEntity).ToString());
            }
        }

        public virtual void Modify(TEntity item)
        {
            if (item != (TEntity)null)
            {
                _UnitOfWork.SetModified(item);
            }
            else
            {
                _logger.LogInformation(LocalizationFactory.CreateLocalResources().GetStringResource(LocalizationKeys.Infraestructure.info_CannotModifyNullEntity), typeof(TEntity).ToString());
            }
        }

        public virtual void Modify(IEnumerable<TEntity> items)
        {
            if (items != null)
            {
                foreach (var item in items) this.Modify(item);
            }
            else
            {
                _logger.LogInformation(LocalizationFactory.CreateLocalResources().GetStringResource(LocalizationKeys.Infraestructure.info_CannotModifyNullEntity), typeof(TEntity).ToString());
            }
        }

        public virtual TEntity Get(object id)
        {
            if (id != null)
            {
                return GetSet().Find(id);
            }
            else
            {
                return null;
            }
        }

        public virtual async Task<TEntity> GetAsync(object id)
        {
            if (id != null)
            {
                return await GetSet().FindAsync(id);
            }
            else
            {
                return null;
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetSet();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetSet().ToListAsync();
        }

        public virtual IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification)
        {
            return GetSet().Where(specification.SatisfiedBy());
        }

        public virtual async Task<IEnumerable<TEntity>> AllMatchingAsync(ISpecification<TEntity> specification)
        {
            return await GetSet().Where(specification.SatisfiedBy()).ToListAsync();
        }

        public virtual TEntity FirstMatching(ISpecification<TEntity> specification)
        {
            return GetSet().FirstOrDefault(specification.SatisfiedBy());
        }

        public virtual async Task<TEntity> FirstMatchingAsync(ISpecification<TEntity> specification)
        {
            return await GetSet().FirstOrDefaultAsync(specification.SatisfiedBy());
        }

        public virtual TEntity FirstMatching<KProperty>(ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.Where(specification.SatisfiedBy()).OrderBy(orderByExpression).FirstOrDefault();
            }
            else
            {
                return set.Where(specification.SatisfiedBy()).OrderByDescending(orderByExpression).FirstOrDefault();
            }
        }

        public virtual async Task<TEntity> FirstMatchingAsync<KProperty>(ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return await set.Where(specification.SatisfiedBy()).OrderBy(orderByExpression).FirstOrDefaultAsync();
            }
            else
            {
                return await set.Where(specification.SatisfiedBy()).OrderByDescending(orderByExpression).FirstOrDefaultAsync();
            }
        }

        public virtual IEnumerable<TEntity> AllMatching<KProperty>(ISpecification<TEntity> specification,
            int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.Where(specification.SatisfiedBy())
                          .OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.Where(specification.SatisfiedBy())
                          .OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> AllMatchingAsync<KProperty>(ISpecification<TEntity> specification, int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return await set.Where(specification.SatisfiedBy())
                          .OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).ToListAsync();
            }
            else
            {
                return await set.Where(specification.SatisfiedBy())
                          .OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).ToListAsync();
            }
        }

        public virtual IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return await set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).ToListAsync();
            }
            else
            {
                return await set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).ToListAsync();
            }
        }

        public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await GetSet().Where(filter).ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetFiltered<KProperty>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending, string includeProperties = "")
        {
            IQueryable<TEntity> set = GetSet();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    set = set.Include(includeProperty);
                }
            }

            if (ascending)
            {
                return set.Where(filter)
                          .OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.Where(filter)
                          .OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync<KProperty>(
                                Expression<Func<TEntity, bool>> filter, 
                                int pageIndex, 
                                int pageCount, 
                                Expression<Func<TEntity, KProperty>> orderByExpression,
                                bool ascending,
                                string includeProperties = "")
        {
            IQueryable<TEntity> set = GetSet();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    set = set.Include(includeProperty);
                }
            }

            if (ascending)
            {
                return await set.Where(filter)
                          .OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).ToListAsync();
            }
            else
            {
                return await set.Where(filter)
                          .OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).ToListAsync();
            }
        }

        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _UnitOfWork.ApplyCurrentValues(persisted, current);
        }

        public virtual void Refresh(TEntity entity)
        {
            _UnitOfWork.Refresh(entity);
        }

        public virtual IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                string includeProperties = "")
        {
            IQueryable<TEntity> query = GetSet();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filter = null,
                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                         string includeProperties = "")
        {
            IQueryable<TEntity> query = GetSet();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            if (_UnitOfWork != null)
            {
                _UnitOfWork.Dispose();
            }
        }
        #endregion
    }
}

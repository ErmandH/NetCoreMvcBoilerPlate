using BoilerPlate.Entity.Entities.Abstract;
using BoilerPlate.Entity.Results.Abstract;
using BoilerPlate.Entity.Results.ComplexTypes;
using BoilerPlate.Entity.Results.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Business.DbServices.Base
{
    public class BaseService<T> where T : class, IEntity, new()
    {
        private readonly DbContext _context;

        public BaseService(DbContext context)
        {
            _context = context;
        }

        // SaveChangesAsync merkezi metod
        protected async Task<IResult> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Değişiklikler başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "Değişiklikler kaydedilirken bir hata oluştu.", ex);
            }
        }

        // Ekleme işlemi
        protected async Task<IResult> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                return await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "Ekleme işlemi sırasında bir hata oluştu.", ex);
            }
        }

        // Silme işlemi
        protected async Task<IResult> DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                return await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "Silme işlemi sırasında bir hata oluştu.", ex);
            }
        }

        // Güncelleme işlemi
        protected async Task<IResult> UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                return await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "Güncelleme işlemi sırasında bir hata oluştu.", ex);
            }
        }

        // Tek bir öğeyi alma
        public async Task<IDataResult<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (predicate != null)
                    query = query.Where(predicate);

                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }

                var entity = await query.SingleOrDefaultAsync();

                if (entity != null)
                {
                    return new DataResult<T>(ResultStatus.Success, entity);
                }

                return new DataResult<T>(ResultStatus.Error, $"{typeof(T).Name} bulunamadı.", null);
            }
            catch (Exception ex)
            {
                return new DataResult<T>(ResultStatus.Error, "Getirme işlemi sırasında bir hata oluştu.", null);
            }
        }

        // Tüm öğeleri listeleme
        public async Task<IDataResult<IList<T>>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (predicate != null)
                    query = query.Where(predicate);

                if (includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }

                var entities = await query.ToListAsync();

                return new DataResult<IList<T>>(ResultStatus.Success, entities);
            }
            catch (Exception ex)
            {
                return new DataResult<IList<T>>(ResultStatus.Error, "Listeleme işlemi sırasında bir hata oluştu.", null);
            }
        }

        // Öğe var mı kontrolü
        public async Task<IDataResult<bool>> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var exists = await _context.Set<T>().AnyAsync(predicate);
                return new DataResult<bool>(ResultStatus.Success, exists);
            }
            catch (Exception ex)
            {
                return new DataResult<bool>(ResultStatus.Error, "Kontrol işlemi sırasında bir hata oluştu.", false);
            }
        }

        // Toplam öğe sayısını alma
        public async Task<IDataResult<int>> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                var count = predicate == null
                    ? await _context.Set<T>().CountAsync()
                    : await _context.Set<T>().CountAsync(predicate);

                return new DataResult<int>(ResultStatus.Success, count);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ResultStatus.Error, "Sayma işlemi sırasında bir hata oluştu.", 0);
            }
        }
    }
}

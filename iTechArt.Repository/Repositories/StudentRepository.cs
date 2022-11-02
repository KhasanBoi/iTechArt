﻿using AutoMapper;
using iTechArt.Database.DbContexts;
using iTechArt.Database.Entities.MedicalStaff;
using iTechArt.Database.Entities.Students;
using iTechArt.Domain.ModelInterfaces;
using iTechArt.Domain.RepositoryInterfaces;
using iTechArt.Repository.BusinessModels;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Dbcontext instance injected
        /// </summary>
        /// <param name="dbContext"></param>
        public StudentRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Add entity to database
        /// </summary>
        /// <param name="entity"></param>   
        public async Task AddAsync(IStudents entity)
        {
            var entry = await _dbContext.Set<StudentDb>().AddAsync(_mapper.Map<StudentDb>(entity));

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get all entities from database
        /// should be remade
        /// </summary>
        public IStudents[] GetAll()
        {
            var models = _dbContext.Set<StudentDb>();

            List<IStudents> result = new List<IStudents>();

            foreach (var i in models)
                result.Add(_mapper.Map<Student>(i));

            return result.ToArray();
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        public async Task<IStudents> GetByIdAsync(long id)
        {
            var databaseModel = await _dbContext.Set<StudentDb>().FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<IStudents>(databaseModel);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        public async Task UpdateAsync(IStudents entity)
        {
            var entry = _dbContext.Set<StudentDb>().Update(_mapper.Map<StudentDb>(entity));

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <param name="entity"></param>
        public async Task DeleteAsync(long id)
        {
            var student = await _dbContext.Students.FirstOrDefaultAsync(d => d.Id == id);

            _dbContext.Students.Remove(_mapper.Map<StudentDb>(student));

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get total count of students
        /// </summary>
        /// <returns></returns>
        public int GetCountOfStudents()
        {
            return _dbContext.Students.Count();
        }
    }
}

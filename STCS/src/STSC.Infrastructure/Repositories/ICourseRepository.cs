﻿using STCS.Infrastructure.Entities;

namespace STCS.Infrastructure.Repositories;

public interface ICourseRepository : IRepository<Course, Guid>
{
    (IList<Course> data, int total, int totalDisplay) GetCourses(int pageIndex,
            int pageSize, string searchText, string orderby);
}
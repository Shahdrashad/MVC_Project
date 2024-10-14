using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAl.Models;

namespace Demo.BLL.Interfaces
{
	public interface IDepartmentRepository:IGenericRepository<Department>
	{
        #region
        //IEnumerable<Department>GetAll();
        //Department GetById(int id);
        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);
        #endregion
    }
}

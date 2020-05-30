using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagerIO.DataModels
{
    public enum RepositoryConnectionContexts
    {
        PublicPortal,
        ControlPanel
    }

    public class RepositoryHelper
    {
        private static RepositoryHelper _Instance;

        public static RepositoryHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new RepositoryHelper();
                }

                return (_Instance);
            }
        }

        public ManagerIORepository GetRepository(bool enableAutoSelect = false, bool enableNamedParams = false)
        {
            ManagerIORepository repository = null;

            repository = new ManagerIORepository("ManagerIO");

            repository.EnableAutoSelect = enableAutoSelect;
            repository.EnableNamedParams = enableNamedParams;

            return (repository);
        }
    }
}

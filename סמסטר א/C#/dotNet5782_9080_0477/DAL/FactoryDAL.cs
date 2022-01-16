﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALException;
using DalFacade;
namespace DAL
{
    public class FactoryDAL
    {
        public static DalFacade.IDal factory(string obj)
        {
            try
            {
                switch (obj)
                {
                    case "DalObject":
                        return DalObject.DalObject.getInstance();
                }
                return DalObject.DalObject.getInstance();
            }
            catch (Exception e)
            {
                throw new CantReturnDalObject(e);
            }
        }
    }
}
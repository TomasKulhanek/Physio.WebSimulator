﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace HumModWebSimulator.Model
{
    [Route("/graphicmetas")]
    public class GraphicMetaDTO : IReturn<List<GraphicMetaDTO>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }



    //only GET 
    public class GraphicMetaService : Service
    {
        public GraphicRepository Repository { get; set; }  //Injected by IOC

        public object Get(GraphicMetaDTO request)
        {
            if (request.Id != 0) return Repository.GetMetaById(request.Id);
            //if (!string.IsNullOrEmpty(request.ReferencedModelName)) return Repository.GetByModelName(request.ReferencedModelName);
            return Repository.GetAllMeta();
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace servicio_semana02.Models
{
    public class Pedido
    {
        [Display(Name = "Id del Pedido")]
        public int idpedido {
            get;
            set;
        }

        [Display(Name ="Fecha del Pedido")]
        public DateTime fechapedido
        {
            get;
            set;
        }

        [Display(Name ="Cliente")]
        public string nombrecia
        {
            get;
            set;
        }

        public string direccion
        {
            get;
            set;
        }

        public string ciudad
        {
            get;
            set;
        }
    }
}
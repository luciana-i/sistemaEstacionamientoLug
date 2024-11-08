﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BL")]

namespace BE
{
    public class Playa : IEnumerable<Espacio>
    {
        public int IdPlaya { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }

        private List<Espacio> ListaPrivadaEspacios = new List<Espacio>();
        public List<Espacio> ListaEspacios 
        {
            get
            {
                
                return ListaPrivadaEspacios;

            }
        }
        public void AgregarEspacio(Espacio espacio, Constantes.EstadosColeccion estado = Constantes.EstadosColeccion.Agregado)
        {
           
                ListaEspacios.Add(espacio);
                espacio.EstadoColeccion = estado;
                espacio.IndiceColeccion = ListaPrivadaEspacios.IndexOf(espacio);
        }


        public void EliminarEspacio(Espacio espacio)
        {
            
            if (ListaEspacios[espacio.IndiceColeccion].EstadoColeccion == Constantes.EstadosColeccion.Agregado)
                ListaEspacios[espacio.IndiceColeccion].EstadoColeccion = Constantes.EstadosColeccion.Quitado;
            else
                ListaEspacios[espacio.IndiceColeccion].EstadoColeccion = Constantes.EstadosColeccion.Eliminado;
        }

        public void ModificarEspacio(Espacio espacio)
        {
           if (espacio.EstadoColeccion != Constantes.EstadosColeccion.Agregado)
                espacio.EstadoColeccion = Constantes.EstadosColeccion.Modificado;
            ListaEspacios[espacio.IndiceColeccion] = espacio;
        }

        public Playa (int idPlaya)
        {
            IdPlaya = idPlaya;

        }

        public Playa()
        {

        }

        public IEnumerator<Espacio> GetEnumerator()
        {
            return ((IEnumerable<Espacio>)ListaEspacios).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)ListaEspacios).GetEnumerator();
        }
    }
}

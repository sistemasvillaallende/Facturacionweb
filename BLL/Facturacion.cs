// Decompiled with JetBrains decompiler
// Type: BLL.Facturacion
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34EC4AF4-6E2A-4DA8-8439-6E4212AC4B99
// Assembly location: H:\NET 2018\Facturacion\bin\BLL.dll

using DAL;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace BLL
{
    public class Facturacion
    {
        public static long insertFacturacion(
          Decimal monto,
          int categoria_deuda,
          string nombre,
          string obs,
          string usu,
          int cod_usuario,
          int cod_oficina,
          long CUIT)
        {
            int num = 0;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                num = DAL.Facturacion.getNroTran() + 1;
                string str = string.Format("F{1}{2}{2}", (object)DateTime.Now.Year.ToString().Remove(0, 2), (object)DateTime.Now.Month, (object)new Random().Next(99));
                DAL.Facturacion.insert(new DAL.Facturacion()
                {
                    categoria_deuda = categoria_deuda,
                    circunscripcion = 0,
                    dominio = string.Empty,
                    exigible = false,
                    fecha_alta = DateTime.Now,
                    fecha_transaccion = DateTime.Now,
                    legajo = 0,
                    manzana = 0,
                    monto = monto,
                    nombre = nombre,
                    nro_bad = 0,
                    nro_transaccion = num,
                    observaciones = obs,
                    pagado = false,
                    parcela = 0,
                    periodo = str,
                    p_h = 0,
                    seccion = 0,
                    usuario = usu,
                    nro_procuracion = 0,
                    cod_oficina = cod_oficina,
                    cod_usuario = cod_usuario,
                    CUIT = CUIT
                });
                CTASCTES_FACTU ctasctesFactu = new CTASCTES_FACTU();
                ctasctesFactu.categoria_deuda = categoria_deuda;
                ctasctesFactu.debe = monto;
                ctasctesFactu.exigible = false;
                ctasctesFactu.fecha_transaccion = DateTime.Now;
                ctasctesFactu.haber = Decimal.Zero;
                ctasctesFactu.monto_original = monto;
                ctasctesFactu.monto_pagado = Decimal.Zero;
                ctasctesFactu.nro_cedulon = 0;
                ctasctesFactu.nro_pago_parcial = 0;
                ctasctesFactu.nro_plan = 0;
                ctasctesFactu.nro_procuracion = 0;
                ctasctesFactu.nro_transaccion = num;
                ctasctesFactu.pagado = false;
                ctasctesFactu.pago_parcial = false;
                ctasctesFactu.periodo = str;
                ctasctesFactu.tipo_transaccion = 1;
                DateTime dateTime = DateTime.Now;

                if (categoria_deuda is 415) {
                    ctasctesFactu.vencimiento = dateTime.AddDays(30.0);
                }
                else
                {
                    ctasctesFactu.vencimiento = dateTime.AddDays(15.0);
                }

                CTASCTES_FACTU.insert(ctasctesFactu);
                Cedulones oCedulon = new Cedulones();
                dateTime = DateTime.Now;
                oCedulon.fecha_emision = dateTime.ToShortDateString();
                oCedulon.subsistema = 2;
                oCedulon.tipo_cedulon = 5;
                oCedulon.no_pagado = true;
                oCedulon.activo = true;
                oCedulon.nro_badec = 0;
                oCedulon.circunscripcion = 0;
                oCedulon.seccion = 0;
                oCedulon.manzana = 0;
                oCedulon.parcela = 0;
                oCedulon.p_h = 0;
                oCedulon.dominio = string.Empty;
                oCedulon.legajo = 0;
                dateTime = DateTime.Now;
                dateTime = dateTime.AddDays(15.0);
                oCedulon.vencimiento_2 = dateTime.ToShortDateString();
                oCedulon.monto_2 = monto;
                oCedulon.contado = monto;
                oCedulon.cheques = Decimal.Zero;
                oCedulon.monto_arreglo = Decimal.Zero;
                Cedulones.insertFacturacion(oCedulon, 0, num, categoria_deuda);
                DAL.Facturacion.setNroTran(num);
                transactionScope.Complete();
            }
            return (long)num;
        }

        public static void delete(int nro_tran)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    CTASCTES_FACTU.delete(nro_tran);
                    DAL.Facturacion.deleta(nro_tran);
                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int getNroCedulon(int nroTran)
        {
            try
            {
                return DAL.Facturacion.getNroCedulon(nroTran);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DAL.Facturacion> getByOficina(int cod)
        {
            try
            {
                return DAL.Facturacion.getByOficina(cod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DAL.Facturacion> getByCUIT(string cuit)
        {
            try
            {
                return DAL.Facturacion.getByCUIT(cuit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DAL.Facturacion getByNroTran(int nroTran)
        {
            try
            {
                return DAL.Facturacion.getByNorTran(nroTran);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

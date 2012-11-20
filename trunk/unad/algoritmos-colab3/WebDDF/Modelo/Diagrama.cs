using Microsoft.VisualBasic;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Text;

namespace WebDDF.Modelo
{
    class Diagrama : IPadreDeOperaciones, ICustomTypeDescriptor, INotifyPropertyChanged
    {
        public const int OperaciónBorde = 1;
        public const int OperaciónMárgen = 3;
        public const int OperaciónLinea = 25;

        const int Márgen = 30;
        const int TamañoAlcance = 40;
        const int EspacioEntreOperaciones = 20;
        public static readonly StringFormat CentroMedio = new StringFormat(StringFormat.GenericDefault);

        static Diagrama()
        {
            CentroMedio.Trimming = StringTrimming.EllipsisCharacter;
            CentroMedio.FormatFlags = StringFormatFlags.NoWrap;
            CentroMedio.Alignment = StringAlignment.Center;
            CentroMedio.LineAlignment = StringAlignment.Center;
        }

        readonly Dictionary<string, object> Variables = new Dictionary<string, object>();
        readonly List<IOperación> operaciones = new List<IOperación>();

        public void Dibujar(Graphics g, Rectangle espacio)
        {
            Point centroArriba = new Point(espacio.X + espacio.Width / 2, espacio.Y);
            DibujarAlcance("Inicio", g, ref centroArriba);
            foreach (IOperación operación in operaciones)
            {
                DibujarConector(g, ref centroArriba);
                operación.Dibujar(g, ref centroArriba);
            }
            DibujarConector(g, ref centroArriba);
            DibujarAlcance("Fin", g, ref centroArriba);
        }

        public static void DibujarConector(Graphics g, ref Point centroArriba)
        {
            Point centroAbajo = centroArriba;
            centroArriba.Y += EspacioEntreOperaciones;
            g.DrawLine(Pens.Black, centroAbajo, centroArriba);
            Point[] flecha = new Point[] {
                centroArriba, 
                new Point(centroArriba.X - 3, centroArriba.Y - 6),
                new Point(centroArriba.X + 3, centroArriba.Y - 6),
            };
            g.FillPolygon(Brushes.Black, flecha, FillMode.Winding);
        }

        public static void DibujarAlcance(string etiqueta, Graphics g, ref Point centroArriba)
        {
            Rectangle rectángulo = new Rectangle(centroArriba.X - TamañoAlcance / 2, centroArriba.Y, TamañoAlcance, TamañoAlcance);
            g.FillEllipse(Brushes.White, rectángulo);
            g.DrawEllipse(Pens.Black, rectángulo);
            g.DrawString(etiqueta, SystemFonts.DefaultFont, Brushes.Black, rectángulo, CentroMedio);
            centroArriba.Y += TamañoAlcance;
        }

        public object Evaluar(string expresión, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(expresión))
            {
                error = "La expresión está vacía.";
                return null;
            }
            CodeDomProvider proveedor = new VBCodeProvider();
            CompilerParameters parámetros = new CompilerParameters()
            {
                GenerateInMemory = true,
                GenerateExecutable = false,
            };
            parámetros.ReferencedAssemblies.Add("System.dll");
            StringBuilder variables = new StringBuilder();
            variables.AppendLine();
            foreach (string variable in Variables.Keys)
            {
                object value = Variables[variable];
                variables.Append("        Dim ");
                variables.Append(variable);
                variables.Append(" As ");
                variables.Append(value.GetType().Name);
                variables.Append(" = ");
                variables.AppendLine(EscaparValor(value));
            }
            string fuente = @"Imports System
Public Class Expresión
    Public Function Evaluar() As Object" + variables.ToString() + @"
        Return " + EscaparExpresión(expresión) + @"
    End Function
End Class";
            CompilerResults resultados = proveedor.CompileAssemblyFromSource(parámetros, fuente);
            if (resultados.Errors.HasErrors)
            {
                error = resultados.Errors[0].ErrorText;
                return null;
            }

            if (resultados.CompiledAssembly == null)
            {
                error = "No es posible generar el ensamblado.";
                return null;
            }

            Type tipoDeExpresión = resultados.CompiledAssembly.GetType("Expresión");
            if (tipoDeExpresión == null)
            {
                error = "No es posible encontrar la clase.";
                return null;
            }

            try
            {
                object instanciaDeExpresión = Activator.CreateInstance(tipoDeExpresión);
                MethodInfo método = tipoDeExpresión.GetMethod("Evaluar");
                object resultado = método.Invoke(instanciaDeExpresión, new object[] { });
                return resultado;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }

        private string EscaparExpresión(string expresión)
        {
            return (expresión ?? string.Empty).Replace("'", "\"").Replace(",", "&");
        }

        static string EscaparValor(object value)
        {
            if (value.GetType() == typeof(string))
            {
                return "\"" + Convert.ToString(value).Replace("\"", "\\\"") + "\"";
            }
            return Convert.ToString(value);
        }

        public IEnumerator<IOperación> GetEnumerator()
        {
            foreach (IOperación operación in operaciones)
            {
                yield return operación;
                if (operación is IEnumerable<IOperación>)
                {
                    foreach (IOperación op in operación as IEnumerable<IOperación>)
                        yield return op;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Agregar(IOperación operación)
        {
            operación.Padre = this;
            operaciones.Add(operación);
        }

        public void InsertarDespuésDe(IOperación operación, IOperación anterior)
        {
            Debug.Assert(anterior.Padre == this);
            operación.Padre = this;
            int indice = operaciones.IndexOf(anterior);
            operaciones.Insert(indice + 1, operación);
        }

        public void Eliminar(IOperación operación)
        {
            Debug.Assert(operación.Padre == this);
            operación.Padre = null;
            operaciones.Remove(operación);
        }

        public bool Ejecutar()
        {
            Variables.Clear();
            foreach (IOperación operación in this)
            {
                if (!operación.Ejecutar(this))
                    return false;
            }
            return true;
        }

        #region Implementation of ICustomTypeDescriptor

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(attributes, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            attributes = new List<Attribute>(attributes ?? new Attribute[] { }) { new CategoryAttribute("Variables") }.ToArray();
            List<PropertyDescriptor> propiedades = new List<PropertyDescriptor>();
            foreach (string variable in this.Variables.Keys)
                propiedades.Add(new CustomPropertyDescriptor(variable, attributes));
            return new PropertyDescriptorCollection(propiedades.ToArray());
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(null);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        #region CustomPropertyDescriptor

        class CustomPropertyDescriptor : PropertyDescriptor
        {
            public CustomPropertyDescriptor(string variable, Attribute[] attributes)
                : base(variable, attributes)
            {
            }

            public override object GetValue(object component)
            {
                var diagrama = component as Diagrama;
                if (diagrama == null)
                    throw new InvalidOperationException();
                return diagrama.Variables[this.Name];
            }

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override Type ComponentType
            {
                get { return typeof(Diagrama); }
            }

            public override bool IsReadOnly
            {
                get { return true; }
            }

            public override Type PropertyType
            {
                get { return typeof(object); }
            }

            public override void ResetValue(object component)
            {
                throw new NotImplementedException();
            }

            public override void SetValue(object component, object value)
            {
                throw new NotImplementedException();
            }

            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public object this[string nombre]
        {
            get
            {
                return Variables[nombre];
            }

            set
            {
                Variables[nombre] = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nombre));
            }
        }

        #endregion
    }
}

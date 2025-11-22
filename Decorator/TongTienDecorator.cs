using System;

namespace CuahangNongduoc.Decorator
{
    /// <summary>
    /// Abstract Decorator: Base class cho tất cả decorators
    /// </summary>
    public abstract class TongTienDecorator : ITongTienComponent
    {
        protected ITongTienComponent m_Component;

        public TongTienDecorator(ITongTienComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException("Component không thể null!");
            }

            m_Component = component;
        }

        public virtual decimal TinhTongTien()
        {
            return m_Component.TinhTongTien();
        }
    }
}

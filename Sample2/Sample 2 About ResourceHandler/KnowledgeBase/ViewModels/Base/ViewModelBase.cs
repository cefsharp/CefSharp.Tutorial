using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace KnowledgeBase.Base
{
	/// <summary>
	/// Class implements a simple viewmodel base class that implements the
	/// <seealso cref="INotifyPropertyChanged"/> interface. Other viewmodel
	/// classes should inherit from it to ensure a consistent implementation
	/// of the <seealso cref="INotifyPropertyChanged"/> interface throughout
	/// a code base.
	/// </summary>
	public class ViewModelBase : INotifyPropertyChanged
	{

		protected virtual void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Implements the standard event of the <seealso cref="INotifyPropertyChanged"/> interface.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Tell bound controls (via WPF binding) to refresh their display.
		/// 
		/// Sample call: this.NotifyPropertyChanged(() => this.IsSelected);
		/// where 'this' is derived from <seealso cref="BaseViewModel"/>
		/// and IsSelected is a property.
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="property"></param>
		public void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> property)
		{
			var lambda = (LambdaExpression)property;
			MemberExpression memberExpression;

			if (lambda.Body is UnaryExpression)
			{
				var unaryExpression = (UnaryExpression)lambda.Body;
				memberExpression = (MemberExpression)unaryExpression.Operand;
			}
			else
				memberExpression = (MemberExpression)lambda.Body;

			this.RaisePropertyChanged(memberExpression.Member.Name);
		}
	}
}

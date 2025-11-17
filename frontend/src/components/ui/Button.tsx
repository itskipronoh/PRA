import React from 'react';
interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: 'primary' | 'secondary' | 'outline' | 'ghost';
  size?: 'sm' | 'md' | 'lg';
  children: React.ReactNode;
  fullWidth?: boolean;
}
export const Button: React.FC<ButtonProps> = ({
  variant = 'primary',
  size = 'md',
  children,
  fullWidth = false,
  className = '',
  ...props
}) => {
  const baseClasses = 'inline-flex items-center justify-center rounded-md font-medium focus:outline-none focus:ring-2 focus:ring-offset-2 transition-colors';
  const variantClasses = {
    primary: 'bg-gradient-to-r from-primary to-secondary text-white shadow-sm hover:from-primary-dark hover:to-secondary-dark focus:ring-primary',
    secondary: 'bg-white border border-gray-300 text-gray-700 shadow-sm hover:bg-gray-50 focus:ring-primary',
    outline: 'bg-transparent border border-primary text-primary hover:bg-primary hover:text-white focus:ring-primary',
    ghost: 'bg-transparent text-primary hover:bg-gray-100 focus:ring-primary'
  };
  const sizeClasses = {
    sm: 'px-3 py-1.5 text-xs',
    md: 'px-4 py-2 text-sm',
    lg: 'px-6 py-3 text-base'
  };
  const widthClass = fullWidth ? 'w-full' : '';
  return <button className={`${baseClasses} ${variantClasses[variant]} ${sizeClasses[size]} ${widthClass} ${className}`} {...props}>
      {children}
    </button>;
};
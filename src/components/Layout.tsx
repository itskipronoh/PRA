import React from 'react';
import { Header } from './Header';
interface LayoutProps {
  children: React.ReactNode;
  title: string;
  subtitle?: string;
}
export const Layout: React.FC<LayoutProps> = ({
  children,
  title,
  subtitle
}) => {
  return <div className="min-h-screen bg-gray-50">
      <Header />
      <main className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
        <div className="px-4 py-6 sm:px-0">
          <div className="mb-6">
            <h1 className="text-2xl font-semibold text-gray-900">{title}</h1>
            {subtitle && <p className="mt-1 text-sm text-gray-500">{subtitle}</p>}
          </div>
          {children}
        </div>
      </main>
    </div>;
};
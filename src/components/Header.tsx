import React from 'react';
import { Link, useLocation } from 'react-router-dom';
export const Header = () => {
  const location = useLocation();
  const navItems = [{
    name: 'Self Assessment',
    path: '/'
  }, {
    name: 'Manager Review',
    path: '/manager-review'
  }, {
    name: 'Meeting Scheduler',
    path: '/meeting-scheduler'
  }, {
    name: 'HR Mediation',
    path: '/hr-mediation'
  }, {
    name: 'Appeal Submission',
    path: '/appeal-submission'
  }, {
    name: 'HR Dashboard',
    path: '/hr-dashboard'
  }];
  return <header className="bg-white border-b border-gray-200 shadow-sm">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between h-16">
          <div className="flex">
            <div className="flex-shrink-0 flex items-center">
              <div className="flex items-center">
                <div className="h-8 w-8 bg-gradient-to-r from-primary to-secondary rounded-md"></div>
                <span className="ml-2 text-xl font-bold text-gray-900">
                  Fintech PMS
                </span>
              </div>
            </div>
          </div>
          <nav className="flex space-x-4 items-center">
            {navItems.map(item => <Link key={item.path} to={item.path} className={`px-3 py-2 text-sm font-medium rounded-md ${location.pathname === item.path ? 'bg-gradient-to-r from-primary to-secondary text-white' : 'text-gray-600 hover:text-primary'}`}>
                {item.name}
              </Link>)}
          </nav>
          <div className="flex items-center">
            <div className="flex items-center space-x-2">
              <div className="h-8 w-8 rounded-full bg-gray-200 flex items-center justify-center">
                <span className="text-sm font-medium text-gray-600">JD</span>
              </div>
              <span className="text-sm font-medium text-gray-700">
                John Doe
              </span>
            </div>
          </div>
        </div>
      </div>
    </header>;
};
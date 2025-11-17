# Performance Management System - API Specification
## Overview
This document provides a comprehensive specification for the backend API of the Performance Management System. It includes data models, API endpoints, authentication requirements, and business logic rules.
---
## Table of Contents
1. [Authentication & Authorization](#authentication--authorization)
2. [Data Models](#data-models)
3. [API Endpoints](#api-endpoints)
4. [Business Logic Rules](#business-logic-rules)
5. [Database Relationships](#database-relationships)
---
## Authentication & Authorization
### User Roles
- **Employee**: Can submit self-assessments, view their reviews, schedule meetings, submit appeals
- **Manager**: Can review employee assessments, provide ratings, schedule meetings
- **HR**: Can view all appraisals, mediate disputes, manage appeals, access dashboard analytics
- **Admin**: Full system access
### Authentication Requirements
- JWT-based authentication
- Role-based access control (RBAC)
- Session management with token refresh
- Password encryption (bcrypt or similar)
---


API Endpoints
Authentication Endpoints
POST /api/auth/login
Description: Authenticate user and return JWT token

Modified Design
Updated
Code Diff
POST /api/auth/refresh
Description: Refresh JWT token

Modified Design
Updated
Code Diff
POST /api/auth/logout
Description: Invalidate user session

POST /api/auth/change-password
Description: Change user password

Modified Design
Updated
Code Diff
User Endpoints
GET /api/users/me
Description: Get current user profile Authorization: All authenticated users

GET /api/users/{id}
Description: Get user by ID Authorization: Managers (for their reports), HR, Admin

GET /api/users
Description: Get all users (with filtering) Authorization: HR, Admin Query Parameters:

department (string)
role (string)
isActive (boolean)
page (number)
limit (number)
PUT /api/users/{id}
Description: Update user profile Authorization: User (own profile), HR, Admin

GET /api/users/{id}/direct-reports
Description: Get all direct reports for a manager Authorization: Manager, HR, Admin
Appraisal Endpoints
POST /api/appraisals
Description: Create new appraisal Authorization: HR, Admin

Modified Design
Updated
Code Diff
GET /api/appraisals/{id}
Description: Get appraisal by ID Authorization: Employee (own), Manager (their reports), HR, Admin

GET /api/appraisals
Description: Get all appraisals (with filtering) Authorization: Manager (their reports), HR, Admin Query Parameters:

employeeId (string)
managerId (string)
status (string)
startDate (date)
endDate (date)
page (number)
limit (number)
PUT /api/appraisals/{id}
Description: Update appraisal Authorization: HR, Admin

GET /api/appraisals/employee/{employeeId}
Description: Get all appraisals for an employee Authorization: Employee (own), Manager, HR, Admin

GET /api/appraisals/manager/{managerId}
Description: Get all appraisals for a manager's reports Authorization: Manager (own), HR, Admin
Self-Assessment Endpoints
POST /api/self-assessments
Description: Create self-assessment Authorization: Employee

Modified Design
Updated
Code Diff
GET /api/self-assessments/{id}
Description: Get self-assessment by ID Authorization: Employee (own), Manager, HR, Admin

PUT /api/self-assessments/{id}
Description: Update self-assessment Authorization: Employee (own, if status is draft)

POST /api/self-assessments/{id}/submit
Description: Submit self-assessment Authorization: Employee (own)

GET /api/self-assessments/appraisal/{appraisalId}
Description: Get self-assessment for an appraisal Authorization: Employee (own), Manager, HR, Admin
Manager Review Endpoints
POST /api/manager-reviews
Description: Create manager review Authorization: Manager

Modified Design
Updated
Code Diff
GET /api/manager-reviews/{id}
Description: Get manager review by ID Authorization: Employee (own), Manager (own), HR, Admin

PUT /api/manager-reviews/{id}
Description: Update manager review Authorization: Manager (own)

POST /api/manager-reviews/{id}/submit
Description: Submit manager review Authorization: Manager (own)

GET /api/manager-reviews/appraisal/{appraisalId}
Description: Get manager review for an appraisal Authorization: Employee (own), Manager, HR, Admin
Meeting Endpoints
POST /api/meetings
Description: Schedule a meeting Authorization: Employee, Manager

Modified Design
Updated
Code Diff
GET /api/meetings/{id}
Description: Get meeting by ID Authorization: Employee (own), Manager (own), HR, Admin

GET /api/meetings
Description: Get all meetings (with filtering) Authorization: Manager, HR, Admin Query Parameters:

employeeId (string)
managerId (string)
status (string)
startDate (date)
endDate (date)
PUT /api/meetings/{id}
Description: Update meeting Authorization: Employee (own), Manager (own)

POST /api/meetings/{id}/complete
Description: Mark meeting as completed Authorization: Manager

Modified Design
Updated
Code Diff
POST /api/meetings/{id}/cancel
Description: Cancel meeting Authorization: Employee (own), Manager (own)

GET /api/meetings/employee/{employeeId}
Description: Get all meetings for an employee Authorization: Employee (own), Manager, HR, Admin
Appeal Endpoints
POST /api/appeals
Description: Submit an appeal Authorization: Employee

Modified Design
Updated
Code Diff
GET /api/appeals/{id}
Description: Get appeal by ID Authorization: Employee (own), HR, Admin

GET /api/appeals
Description: Get all appeals (with filtering) Authorization: HR, Admin Query Parameters:

employeeId (string)
status (string)
priority (string)
startDate (date)
endDate (date)
PUT /api/appeals/{id}
Description: Update appeal Authorization: Employee (own, if status is draft), HR, Admin

POST /api/appeals/{id}/review
Description: Review an appeal (HR action) Authorization: HR, Admin

Modified Design
Updated
Code Diff
GET /api/appeals/employee/{employeeId}
Description: Get all appeals for an employee Authorization: Employee (own), HR, Admin
Mediation Endpoints
POST /api/mediations
Description: Create mediation session Authorization: HR, Admin

Modified Design
Updated
Code Diff
GET /api/mediations/{id}
Description: Get mediation by ID Authorization: Employee (own), Manager (own), HR, Admin

GET /api/mediations
Description: Get all mediations Authorization: HR, Admin Query Parameters:

appealId (string)
status (string)
hrRepresentativeId (string)
PUT /api/mediations/{id}
Description: Update mediation Authorization: HR, Admin

POST /api/mediations/{id}/complete
Description: Complete mediation session Authorization: HR, Admin

Modified Design
Updated
Code Diff
Goal Endpoints
POST /api/goals
Description: Create a goal Authorization: Employee, Manager

Modified Design
Updated
Code Diff
GET /api/goals/{id}
Description: Get goal by ID Authorization: Employee (own), Manager, HR, Admin

GET /api/goals/employee/{employeeId}
Description: Get all goals for an employee Authorization: Employee (own), Manager, HR, Admin

PUT /api/goals/{id}
Description: Update goal Authorization: Employee (own), Manager

PUT /api/goals/{id}/progress
Description: Update goal progress Authorization: Employee (own), Manager

Modified Design
Updated
Code Diff
DELETE /api/goals/{id}
Description: Delete goal Authorization: Employee (own), Manager, HR, Admin
Document Endpoints
POST /api/documents/upload
Description: Upload a document Authorization: All authenticated users

Modified Design
Updated
Code Diff
GET /api/documents/{id}
Description: Get document by ID Authorization: Authorized users based on linked entity

GET /api/documents
Description: Get all documents (with filtering) Authorization: HR, Admin Query Parameters:

appraisalId (string)
appealId (string)
uploadedBy (string)
DELETE /api/documents/{id}
Description: Delete document Authorization: Uploader, HR, Admin
Notification Endpoints
GET /api/notifications
Description: Get all notifications for current user Authorization: All authenticated users Query Parameters:

isRead (boolean)
type (string)
page (number)
limit (number)
GET /api/notifications/unread-count
Description: Get count of unread notifications Authorization: All authenticated users

PUT /api/notifications/{id}/read
Description: Mark notification as read Authorization: All authenticated users

PUT /api/notifications/mark-all-read
Description: Mark all notifications as read Authorization: All authenticated users
Dashboard & Analytics Endpoints
GET /api/dashboard/hr/overview
Description: Get HR dashboard overview Authorization: HR, Admin

Modified Design
Updated
Code Diff
GET /api/dashboard/manager/overview
Description: Get manager dashboard overview Authorization: Manager

Modified Design
Updated
Code Diff
GET /api/dashboard/employee/overview
Description: Get employee dashboard overview Authorization: Employee

Modified Design
Updated
Code Diff
GET /api/analytics/ratings-distribution
Description: Get distribution of ratings across organization Authorization: HR, Admin Query Parameters:

department (string)
startDate (date)
endDate (date)
GET /api/analytics/completion-rate
Description: Get appraisal completion rate over time Authorization: HR, Admin Query Parameters:

period (string: 'monthly', 'quarterly', 'yearly')
Business Logic Rules
Appraisal Workflow
Creation: HR creates appraisal for employee with review period
Self-Assessment: Employee completes self-assessment
Manager Review: Manager completes review after self-assessment is submitted
Meeting: Manager and employee schedule and complete review meeting
Completion: Appraisal marked as completed after meeting
Appeal (optional): Employee can appeal within 14 days of completion
Status Transitions
Appraisal: draft → submitted → under_review → completed → appealed (optional)
Appeal: submitted → under_review → approved/rejected/pending_mediation
Meeting: scheduled → completed/cancelled/rescheduled
Mediation: pending → scheduled → in_progress → completed
Validation Rules
Self-Assessment: Cannot be submitted if any required field is empty
Manager Review: Can only be submitted after self-assessment is submitted
Ratings: All ratings must be between 1-5
Meeting: Cannot be scheduled in the past
Appeal: Can only be submitted within 14 days of appraisal completion
Documents: Maximum file size 10MB, allowed types: PDF, DOC, DOCX, JPG, PNG
Access Control Rules
Employee: Can only view/edit their own appraisals, appeals, and meetings
Manager: Can view/edit appraisals for direct reports only
HR: Can view all appraisals, appeals, and mediate disputes
Admin: Full access to all resources
Notification Triggers
Appraisal created → Notify employee and manager
Self-assessment submitted → Notify manager
Manager review submitted → Notify employee
Meeting scheduled → Notify both parties
Appeal submitted → Notify HR
Appeal reviewed → Notify employee
Mediation scheduled → Notify all parties
Database Relationships
Entity Relationships
User (1) ----< (Many) Appraisal [as employee] User (1) ----< (Many) Appraisal [as manager] User (1) ----< (Many) User [as manager to direct reports] Appraisal (1) ---- (1) SelfAssessment Appraisal (1) ---- (1) ManagerReview Appraisal (1) ----< (Many) Meeting Appraisal (1) ----< (Many) Goal Appraisal (1) ----< (Many) Appeal Appraisal (1) ----< (Many) Document Appeal (1) ----< (Many) Mediation Appeal (1) ----< (Many) Document User (1) ----< (Many) Notification User (1) ----< (Many) AuditLog

Indexes Recommendations
User: email (unique), employeeId (unique), managerId
Appraisal: employeeId, managerId, status, reviewPeriodEnd
SelfAssessment: appraisalId (unique), employeeId
ManagerReview: appraisalId (unique), managerId
Meeting: employeeId, managerId, scheduledDate, status
Appeal: appraisalId, employeeId, status, submittedAt
Goal: employeeId, appraisalId, status
Notification: userId, isRead, createdAt
AuditLog: userId, entityType, entityId, createdAt




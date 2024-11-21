import { Routes } from '@angular/router';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { AdminDashboardComponent } from './components/admin/admin-dashboard/admin-dashboard.component';
import { ResetComponent } from './components/reset/reset.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { UserComponent } from './components/user/user.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { LogoutComponent } from './components/user/logout/logout.component';
import { JobsComponent } from './components/user/jobs/jobs.component';
import { AplliedComponent } from './components/user/apllied/apllied.component';
import { ApprovedComponent } from './components/user/approved/approved.component';
import { ManagerComponent } from './components/manager/manager.component';
import { ManagerLogoutComponent } from './components/manager/manager-logout/manager-logout.component';
import { ManagerProfileComponent } from './components/manager/manager-profile/manager-profile.component';
import { ManagerDashboardComponent } from './components/manager/manager-dashboard/manager-dashboard.component';
import { ManagerJobsComponent } from './components/manager/manager-jobs/manager-jobs.component';
import { ManagerApplicationsComponent } from './components/manager/manager-applications/manager-applications.component';
import { AdminComponent } from './components/admin/admin.component';
import { UsersComponent } from './components/admin/users/users.component';

export const routes: Routes = [
    {path: '' , component:LandingPageComponent},
    {path: 'register' , component:RegistrationComponent},
    {path: 'login' , component:LoginComponent},
    {path: 'resetPassword', component:ResetComponent},
    {path: 'notFound', component:NotFoundComponent},
    {path: 'user', component:UserComponent, children: [
        {path: 'profile', component: ProfileComponent},
        {path: 'jobs', component: JobsComponent},
        {path: 'applied', component: AplliedComponent},
        {path: 'logout', component: LogoutComponent},
        {path: 'approved', component: ApprovedComponent},
        {path: '', redirectTo: 'jobs',pathMatch: 'full'}
    ]},
    {path: 'manager', component:ManagerComponent , children: [
        {path: 'dashboard', component: ManagerDashboardComponent},
        {path: 'profile', component: ManagerProfileComponent},
        {path: 'jobs', component: ManagerJobsComponent},
        {path: 'applications', component: ManagerApplicationsComponent},
        {path: 'logout', component: ManagerLogoutComponent},
        {path: '', redirectTo: 'dashboard',pathMatch: 'full'}
    ]},
    {path: 'admin', component:AdminComponent , children: [
        {path: 'dashboard', component: AdminDashboardComponent},
        {path: 'profile', component:ProfileComponent },
        {path: 'users', component: UsersComponent},
        {path: 'jobs', component: JobsComponent},
        {path: 'logout', component: LogoutComponent},
        {path: '', redirectTo: 'dashboard',pathMatch: 'full'}
    ]}
];

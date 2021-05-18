import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AbpHttpInterceptor } from 'abp-ng2-module';

import * as ApiServiceProxies from './service-proxies';

@NgModule({
    providers: [
        ApiServiceProxies.RoleServiceProxy,
        ApiServiceProxies.SessionServiceProxy,
        ApiServiceProxies.TenantServiceProxy,
        ApiServiceProxies.UserServiceProxy,
        ApiServiceProxies.TokenAuthServiceProxy,
        ApiServiceProxies.AccountServiceProxy,
        ApiServiceProxies.ConfigurationServiceProxy,
        ApiServiceProxies.CategoryServiceProxy,
        ApiServiceProxies.PublisherServiceProxy,
        ApiServiceProxies.AuthorServiceProxy,
        ApiServiceProxies.ProvinceServiceProxy,
        ApiServiceProxies.DistrictServiceProxy,
        ApiServiceProxies.BookLibraryServiceProxy,
        ApiServiceProxies.BookServiceProxy,
        ApiServiceProxies.BorrowBookDetaiServiceProxy,
        ApiServiceProxies.BorrowBookServiceProxy,
        ApiServiceProxies.LibraryServiceProxy,
        ApiServiceProxies.FavoriteServiceProxy,
        ApiServiceProxies.StatisticServiceProxy,
        ApiServiceProxies.LibraryProvinceServiceProxy,
        { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true }
    ]
})
export class ServiceProxyModule { }

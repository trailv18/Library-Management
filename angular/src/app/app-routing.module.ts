import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { CategoryComponent } from './category/category.component';
import { AuthorComponent } from './author/author.component';
import { PublisherComponent } from './publisher/publisher.component';
import { ProvinceComponent } from './province/province.component';
import { DistrictComponent } from './district/district.component';
import { BookComponent } from './book/book.component';
import { LibrariesComponent } from './libraries/libraries.component';
import { BookLibraryComponent } from './book-library/book-library.component';
import { ReaderLibraryComponent } from './reader-library/reader-library.component';
import { BookListComponent } from './reader-library/book-list/book-list.component';
import { BookDetailComponent } from './reader-library/book-detail/book-detail.component';
import { ReaderAddedComponent } from './reader-added/reader-added.component';
import { BorrowbookComponent } from './borrowbook/borrowbook.component';
import { BorrowbookDetailComponent } from './borrowbook/borrowbook-detail/borrowbook-detail.component';
import { ReaderBorrowComponent } from './reader-borrow/reader-borrow.component';
import { ReaderBorrowDetailComponent } from './reader-borrow/reader-borrow-detail/reader-borrow-detail.component';
import { ReaderFavoritebookComponent } from './reader-favoritebook/reader-favoritebook.component';
import { StatisticComponent } from './statistic/statistic.component';
import { LibraryProvinceDistrictComponent } from './library-province-district/library-province-district.component';
import { StatisticReportCategoryBorrowComponent } from './statistic-report-category-borrow/statistic-report-category-borrow.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent },
                    { path: 'update-password', component: ChangePasswordComponent },
                    { path: 'categories', component: CategoryComponent},
                    { path: 'authors', component: AuthorComponent},
                    { path: 'publishers', component: PublisherComponent},
                    { path: 'provinces', component: ProvinceComponent},
                    { path: 'district/:id', component: DistrictComponent},
                    { path: 'books', component: BookComponent},
                    { path: 'libraries', component: LibrariesComponent},
                    { path: 'library-book-list/:id', component: BookLibraryComponent},
                    { path: 'borrow-book', component: BorrowbookComponent},
                    { path: 'borrow-book-detail/:id', component: BorrowbookDetailComponent},
                    { path: 'reader-library', component: ReaderLibraryComponent},
                    { path: 'book-list/:id', component: BookListComponent},
                    { path: 'book-detail/:id', component: BookDetailComponent},
                    { path: 'reader-list-added', component: ReaderAddedComponent},
                    { path: 'reader-borrow', component: ReaderBorrowComponent},                 
                    { path: 'reader-borrow-detail/:id', component: ReaderBorrowDetailComponent},
                    { path: 'favorite-book', component: ReaderFavoritebookComponent}, 
                    { path: 'statistic/:id', component: StatisticComponent},    
                    { path: 'library-province-district', component: LibraryProvinceDistrictComponent},    
                    { path: 'statistics', component: StatisticReportCategoryBorrowComponent},  
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }

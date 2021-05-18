import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { HomeComponent } from '@app/home/home.component';
import { AboutComponent } from '@app/about/about.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ChartsModule } from 'ng2-charts';

// tenants
import { TenantsComponent } from '@app/tenants/tenants.component';
import { CreateTenantDialogComponent } from './tenants/create-tenant/create-tenant-dialog.component';
import { EditTenantDialogComponent } from './tenants/edit-tenant/edit-tenant-dialog.component';
// roles
import { RolesComponent } from '@app/roles/roles.component';
import { CreateRoleDialogComponent } from './roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from './roles/edit-role/edit-role-dialog.component';
// users
import { UsersComponent } from '@app/users/users.component';
import { CreateUserDialogComponent } from '@app/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ResetPasswordDialogComponent } from './users/reset-password/reset-password.component';
// layout
import { HeaderComponent } from './layout/header.component';
import { HeaderLeftNavbarComponent } from './layout/header-left-navbar.component';
import { HeaderLanguageMenuComponent } from './layout/header-language-menu.component';
import { HeaderUserMenuComponent } from './layout/header-user-menu.component';
import { FooterComponent } from './layout/footer.component';
import { SidebarComponent } from './layout/sidebar.component';
import { SidebarLogoComponent } from './layout/sidebar-logo.component';
import { SidebarUserPanelComponent } from './layout/sidebar-user-panel.component';
import { SidebarMenuComponent } from './layout/sidebar-menu.component';
import { CategoryComponent } from './category/category.component';
import { CreateCategoryComponent } from './category/create-category/create-category.component';
import { EditCategoryComponent } from './category/edit-category/edit-category.component';
import { AuthorComponent } from './author/author.component';
import { CreateAuthorComponent } from './author/create-author/create-author.component';
import { EditAuthorComponent } from './author/edit-author/edit-author.component';
import { PublisherComponent } from './publisher/publisher.component';
import { CreatePublisherComponent } from './publisher/create-publisher/create-publisher.component';
import { EditPublisherComponent } from './publisher/edit-publisher/edit-publisher.component';
import { ProvinceComponent } from './province/province.component';
import { CreateProvinceComponent } from './province/create-province/create-province.component';
import { EditProvinceComponent } from './province/edit-province/edit-province.component';
import { DistrictComponent } from './district/district.component';
import { CreateDistrictComponent } from './district/create-district/create-district.component';
import { EditDistrictComponent } from './district/edit-district/edit-district.component';
import { BookComponent } from './book/book.component';
import { CreateBookComponent } from './book/create-book/create-book.component';
import { EditBookComponent } from './book/edit-book/edit-book.component';
import { LibrariesComponent } from './libraries/libraries.component';
import { CreateLibraryComponent } from './libraries/create-library/create-library.component';
import { EditLibraryComponent } from './libraries/edit-library/edit-library.component';
import { BookLibraryComponent } from './book-library/book-library.component';
import { CreateBooklibraryComponent } from './book-library/create-book-library/create-book-library.component';
import { EditBookLibraryComponent } from './book-library/edit-book-library/edit-book-library.component';
import { ReaderLibraryComponent } from './reader-library/reader-library.component';
import { BookListComponent } from './reader-library/book-list/book-list.component';
import { BookDetailComponent } from './reader-library/book-detail/book-detail.component';
import { ReaderAddedComponent } from './reader-added/reader-added.component';
import { BorrowbookComponent } from './borrowbook/borrowbook.component';
import { BorrowbookDetailComponent } from './borrowbook/borrowbook-detail/borrowbook-detail.component';
import { UpdateStatusComponent } from './borrowbook/borrowbook-detail/update-status/update-status.component';
import { ReaderBorrowComponent } from './reader-borrow/reader-borrow.component';
import { ReaderBorrowDetailComponent } from './reader-borrow/reader-borrow-detail/reader-borrow-detail.component';
import { ReaderFavoritebookComponent } from './reader-favoritebook/reader-favoritebook.component';
import { StatisticComponent } from './statistic/statistic.component';
import { LibraryProvinceDistrictComponent } from './library-province-district/library-province-district.component';
import { StatisticReportCategoryBorrowComponent } from './statistic-report-category-borrow/statistic-report-category-borrow.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    // tenants
    TenantsComponent,
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    RolesComponent,
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    UsersComponent,
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ChangePasswordComponent,
    ResetPasswordDialogComponent,
    // layout
    HeaderComponent,
    HeaderLeftNavbarComponent,
    HeaderLanguageMenuComponent,
    HeaderUserMenuComponent,
    FooterComponent,
    SidebarComponent,
    SidebarLogoComponent,
    SidebarUserPanelComponent,
    SidebarMenuComponent,
    CategoryComponent,
    CreateCategoryComponent,
    EditCategoryComponent,
    AuthorComponent,
    CreateAuthorComponent,
    EditAuthorComponent,
    PublisherComponent,
    CreatePublisherComponent,
    EditPublisherComponent,
    ProvinceComponent,
    CreateProvinceComponent,
    EditProvinceComponent,
    DistrictComponent,
    CreateDistrictComponent,
    EditDistrictComponent,
    BookComponent,
    CreateBookComponent,
    EditBookComponent,
    LibrariesComponent,
    CreateLibraryComponent,
    EditLibraryComponent,
    BookLibraryComponent,
    CreateBooklibraryComponent,
    EditBookLibraryComponent,
    ReaderLibraryComponent,
    BookListComponent,
    BookDetailComponent,
    ReaderAddedComponent,
    BorrowbookComponent,
    BorrowbookDetailComponent,
    UpdateStatusComponent,
    ReaderBorrowComponent,
    ReaderBorrowDetailComponent,
    ReaderFavoritebookComponent,
    StatisticComponent,
    LibraryProvinceDistrictComponent,
    StatisticReportCategoryBorrowComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ModalModule.forChild(),
    BsDropdownModule,
    CollapseModule,
    TabsModule,
    AppRoutingModule,
    ServiceProxyModule,
    SharedModule,
    NgxPaginationModule,
    BsDatepickerModule.forRoot(),
    ChartsModule,
  ],
  providers: [],
  entryComponents: [
    // tenants
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ResetPasswordDialogComponent,
  ],
})
export class AppModule {}

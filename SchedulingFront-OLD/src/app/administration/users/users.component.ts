import { Component, ViewChild, Injector } from '@angular/core';
import { FormControl } from '@angular/forms';
import { User } from 'src/app/common/models/user';
import { UserService } from 'src/app/services/user.service';
import { LocalData } from 'src/app/common/data/localData';
import { ModalFactoryModel } from 'src/app/common/modals/modalBase/modalFactory/modalFactory.component';
import { PasswordChangeComponent } from './passwordChange/passwordChange.component';
import { DataGridComponent } from 'src/app/common/components/dataGrid/dataGrid.component';
import { ListFormBase } from 'src/app/common/base/listFormBase';

@Component({
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent extends ListFormBase<User> {
  @ViewChild('userGrid') userGrid: DataGridComponent<User>;
  constructor(private inj: Injector, private userService: UserService) {
    super(inj,
          'user',
          ['firstName', 'lastName', 'identificationNumber', 'active', 'roles', 'sysDTCreated', 'actions'],
          (p) => this.userService.getUsers(p),
          (id) => this.userService.delete(id));
    this.filters = this.fb.group({
      firstName: new FormControl(),
      lastName: new FormControl()
    });
  }

  onRowClickLink = (user: User) => '/edit/' + user.userId;

  changePass(index: number) {
    this.userGrid.setCacheActivePage(index);
    LocalData.setReturnUrl(this.router.url);
    this.modal.openComponent(new ModalFactoryModel(PasswordChangeComponent, 'label_password_change'), 'password-change-modal');
  }
}

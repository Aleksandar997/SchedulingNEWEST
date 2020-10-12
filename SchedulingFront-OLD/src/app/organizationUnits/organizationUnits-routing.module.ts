import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { actionEnum } from '../common/enums';
import { OrganizationUnitsComponent } from './organizationUnits.component';
import { ActionComponent } from './action/action.component';

const routes: Routes = [
    {
        path: '',
        component: OrganizationUnitsComponent,
        data: {
            title: 'title_organizationUnits'
        }
    },
    {
        path: 'add',
        component: ActionComponent,
        data: {
            title: 'title_add_organizationUnit',
            action: actionEnum.Add
        }
    },
    {
        path: 'view/:id',
        component: ActionComponent,
        data: {
            title: 'title_view_organizationUnit',
            action: actionEnum.View
        }
    },
    {
        path: 'edit/:id',
        component: ActionComponent,
        data: {
            title: 'title_edit_organizationUnit',
            action: actionEnum.Edit
        }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class OrganizationUnitsRoutingModule {

}


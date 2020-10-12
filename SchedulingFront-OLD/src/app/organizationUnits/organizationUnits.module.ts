import { NgModule } from '@angular/core';
import { OrganizationUnitsComponent } from './organizationUnits.component';
import { OrganizationUnitsRoutingModule } from './organizationUnits-routing.module';
import { ActionComponent } from './action/action.component';
import { RouterModule } from '@angular/router';
import { TranslatePipeModule } from '../common/pipes/translate/translatePipe.module';
import { PortalOutletDirectiveModule } from '../common/directives/portalOutlet/portalOutlet.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DataGridModule } from '../common/components/dataGrid/dataGrid.module';
import {
  MatTableModule,
  MatPaginatorModule,
  MatTooltipModule,
  MatInputModule,
  MatIconModule,
  MatButtonModule,
  MatDialogModule,
  MatCheckboxModule,
  MatSelectModule,
  MatFormFieldModule,
  MatSortModule
} from '@angular/material';
import { LoaderModule } from '../common/components/loader/loader.module';
import { ModalBaseModule } from '../common/modals/modalBase/modalBase.module';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';

@NgModule({
  declarations: [
    OrganizationUnitsComponent,
    ActionComponent
  ],
  imports: [
    OrganizationUnitsRoutingModule,
    RouterModule,
    TranslatePipeModule,
    PortalOutletDirectiveModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    DataGridModule,
    MatTableModule,
    MatPaginatorModule,
    MatTooltipModule,
    MatInputModule,
    LoaderModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatCheckboxModule,
    MatSelectModule,
    MatFormFieldModule,
    ModalBaseModule,
    MatSortModule,
    LeafletModule
  ],
  providers: [],
  bootstrap: []
})
export class OrganizationUnitsModule { }

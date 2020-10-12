import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material';
import { FullPageLoaderComponent } from './fullPageLoader.component';

@NgModule({
  declarations: [
    FullPageLoaderComponent
  ],
  imports: [
    CommonModule,
    MatProgressSpinnerModule
  ],
  providers: [],
  bootstrap: [],
  exports: [
    FullPageLoaderComponent
  ]
})
export class FullPageLoaderModule { }

import { Component, Injector, AfterViewInit } from '@angular/core';
import { OrganizationUnit } from 'src/app/models/organizationUnit';
import { ActionFormBase } from 'src/app/common/base/actionFormBase';
import { OrganizationUnitService } from 'src/app/services/organization-unit.service';
import { FormGroupHelper } from 'src/app/common/helpers/formGroupHelper';
import { ResponseBase } from 'src/app/common/models/responseBase';
import { FormControl, Validators } from '@angular/forms';
import { ActionType } from 'src/app/common/base/formBase';
import { tileLayer, latLng } from 'leaflet';
// import * as L from 'leaflet';
// import 'leaflet-control-geocoder';
declare const L: any;
import 'leaflet';
import 'leaflet-control-geocoder';

@Component({
  templateUrl: './action.component.html',
  styleUrls: ['./action.component.css']
})
export class ActionComponent extends ActionFormBase<OrganizationUnit> implements AfterViewInit {
  // searchControl = L.Control['Geocoder'].geosearch();
  form = this.fb.group({
    organizationUnitId: new FormControl(null),
    name: new FormControl({ value: null, disabled: this.areControlsDisabled() }, [Validators.required]),
    code: new FormControl({ value: null, disabled: this.areControlsDisabled() }, [Validators.required]),
    active: new FormControl({ value: null, disabled: this.areControlsDisabled() }, [Validators.required]),
    bindScheduleToEmployee: new FormControl({ value: null, disabled: this.areControlsDisabled() }, [Validators.required])
  });
  options = {
    layers: [
      tileLayer('https://{s}.tile.osm.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://osm.org/copyright">OpenStreetMap</a> contributors'
      })
    ],
    // geocoder: L.Control['Geocoder']().nominatim(),
    zoom: 7,
    center: latLng([ 46.879966, -121.726909 ])
  };

  constructor(private inj: Injector, private service: OrganizationUnitService) {
  super(inj, 'OrganizationUnit', 'organizationunits');
}

  ngAfterViewInit(): void {
    if (this.getId()) {
      this.getById(organizationUnitId => {
        return this.service.getById(organizationUnitId).then(res => {
          FormGroupHelper.mapObjectToFormGroup(res.data, this.form);
        }) as Promise<ResponseBase<OrganizationUnit>>;
      });
    }
  }

  onMapReady(map: L.Map) {
    L.Control.geocoder(this.options);
    // setTimeout(() => {
    //   map.invalidateSize(true);
    //   this.searchControl.addTo(map);
    // }, 0);
  }

  submit() {
    this.form.getControls('organizationUnitId').setValue(this.getId());
    return this.execFunc(() => {
      return this.service.save(this.form.getRawValue());
    }, ActionType.Save, this.form);
  }
}













  //   streetMaps = tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
  //     detectRetina: true,
  //     attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
  //   });
  //   wMaps = tileLayer('http://maps.wikimedia.org/osm-intl/{z}/{x}/{y}.png', {
  //     detectRetina: true,
  //     attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
  //   });

  //   summit = marker([ 46.8523, -121.7603 ], {
  //     icon: icon({
  //       iconSize: [ 25, 41 ],
  //       iconAnchor: [ 13, 41 ],
  //       iconUrl: 'leaflet/marker-icon.png',
  //       shadowUrl: 'leaflet/marker-shadow.png'
  //     })
  //   });

  //   paradise = marker([ 46.78465227596462,-121.74141269177198 ], {
  //     icon: icon({
  //       iconSize: [ 25, 41 ],
  //       iconAnchor: [ 13, 41 ],
  //       iconUrl: 'leaflet/marker-icon.png',
  //       iconRetinaUrl: 'leaflet/marker-icon-2x.png',
  //       shadowUrl: 'leaflet/marker-shadow.png'
  //     })
  //   });
  
  //   route = polyline([[ 46.78465227596462,-121.74141269177198 ],
  //     [ 46.80047278292477, -121.73470708541572 ],
  //     [ 46.815471360459924, -121.72521826811135 ],
  //     [ 46.8360239546746, -121.7323131300509 ],
  //     [ 46.844306448474526, -121.73327445052564 ],
  //     [ 46.84979408048093, -121.74325201660395 ],
  //     [ 46.853193528950214, -121.74823296256363 ],
  //     [ 46.85322881676257, -121.74843915738165 ],
  //     [ 46.85119913890958, -121.7519719619304 ],
  //     [ 46.85103829018772, -121.7542376741767 ],
  //     [ 46.85101557523012, -121.75431755371392 ],
  //     [ 46.85140013694763, -121.75727385096252 ],
  //     [ 46.8525277543813, -121.75995212048292 ],
  //     [ 46.85290292836726, -121.76049157977104 ],
  //     [ 46.8528160918504, -121.76042997278273 ]]);
 
  //   layersControl = {
  //     baseLayers: {
  //       'Street Maps': this.streetMaps,
  //       'Wikimedia Maps': this.wMaps
  //     },
  //     overlays: {
  //       'Mt. Rainier Summit': this.summit,
  //       'Mt. Rainier Paradise Start': this.paradise,
  //       'Mt. Rainier Climb Route': this.route
  //     }
  //   };
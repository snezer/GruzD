<template>
  <div>
        <Galleria :value="photosProduct" :numVisible="4" style="max-width: 500px"
                  :showItemNavigators="true" :showThumbnails="false">
          <template #item="slotProps">
            <img :src="slotProps.item.itemImageSrc" :alt="slotProps.item.alt" style="width: 100%; display: block;" />
          </template>
        </Galleria>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from "vue-property-decorator";
@Component({
  props: {
    photosTrainCar: Array,
    photosProduct: Array,
  }
})
export default class AcceptanceGalleria extends Vue{
  responsiveOptions2: [
    {
      breakpoint: '768px',
      numVisible: 3
    },
    {
      breakpoint: '560px',
      numVisible: 1
    }
  ]
};
</script>

<style>
.p-galleria-indicator-onitem.p-galleria-indicators-right .p-galleria-indicators{
  height: max-content !important;
}
.p-tabview .p-tabview-nav{
  background: unset;
  border: none;
}
.p-tabview .p-tabview-nav li.p-highlight .p-tabview-nav-link{
  background: #B94A50;
  color: white;
  border-color: unset;
}
.p-tabview .p-tabview-nav li .p-tabview-nav-link{
  background: #5C2D3D;
  color: white;
  border-color: unset;
}
.p-tabview .p-tabview-panels{
  padding: 0;
}

</style>

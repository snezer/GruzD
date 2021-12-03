<template>
  <div>
    <TabView>
      <TabPanel header="Товар">
        <Galleria :value="photosProduct" :responsiveOptions="responsiveOptions2" :numVisible="4" thumbnailsPosition="right" style="max-width: 500px"

                  :showThumbnails="false" :showIndicators="true" :changeItemOnIndicatorHover="true" :showIndicatorsOnItem="true" indicatorsPosition="right">
          <template #item="slotProps">
            <img :src="slotProps.item.itemImageSrc" :alt="slotProps.item.alt" style="width: 100%; display: block;" />
          </template>
          <template #indicator="{index}">
            <div style="width:54px; height: 51px; color: #e9ecef; display: flex; justify-content: center; align-items:center; cursor: pointer">
                {{index + 1}}
            </div>
          </template>
        </Galleria>
      </TabPanel>
      <TabPanel header="№ Вагона">

      </TabPanel>
    </TabView>
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
}
.p-tabview .p-tabview-nav li.p-highlight .p-tabview-nav-link{
  background: #B94A50;
  color: white;
  border-color: unset;
}
</style>

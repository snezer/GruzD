<template>
  <div>
    <div class="p-grid">
      <div class="p-col-12">
        <div class="p-grid">
          <div class="p-col-6">
            {{acceptance.supplier.name}}
          </div>
          <div class="p-col-6 p-text-right">
            Зона {{acceptance.dischargeZone}}
            Вагон {{acceptance.numberTrainCar}}
          </div>
        </div>
      </div>
      <div class="p-col-12">
        <div class="p-grid">
          <div class="p-col-6">
          Разгружен на {{acceptance.status}}
          </div>
          <div class="p-col-6 p-text-right">

          </div>
        </div>
      </div>
      <div class="p-col-12">
        <div class="p-grid">
          <div class="p-col-6" style="color: #F15B5B;">
            Обнаруженно {{acceptance.defective}} брака
          </div>
          <div class="p-col-6 p-text-right">
            {{acceptance.product.name}}
          </div>
        </div>
      </div>
    </div>
    <AcceptanceGalleria :photos-product="photosProduct" />
  </div>
</template>

<script>
import { Component, Vue} from "vue-property-decorator";
import AcceptanceGalleria from "@/components/acceptance/AcceptanceGalleria";
@Component({
  components: { AcceptanceGalleria },
  props:{
    acceptance: {
      type: Object,
      required: true,
    }
  }
})
export default class Acceptance extends Vue {

  get photosProduct() {
    return this?.acceptance.photos.map( photo=>{
      return {
        itemImageSrc: photo,
        thumbnailImageSrc: photo,
        alt: "Description for Image 1",
        title: "Title 1"
      }
    })
  }
};
</script>

<style scoped>

</style>

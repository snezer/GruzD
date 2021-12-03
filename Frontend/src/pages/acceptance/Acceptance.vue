<template>
  <div>
    <AcceptanceGalleria :photos-product="photosProduct" />
    <div class="p-grid">
      <div class="p-col-12">
        Результат
      </div>
      <div class="p-col-12">
        Дата {{new Date(acceptance.date).toLocaleDateString('ru', {year: "numeric", month: "2-digit", day: "2-digit", hour: "2-digit", minute: "2-digit", second: "2-digit"})}}
      </div>
      <div class="p-col-12">
        Обнаруженно {{acceptance.defective}} брака
      </div>
    </div>
    <div class="p-grid" style="margin-top: 10px">
      <div class="p-col-6 p-text-center">
        <Button class="p-button-outlined p-button-danger p-button-lg" label="Ошибка"></Button>
      </div>
    </div>
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

<style>
.p-datatable .p-datatable-thead > tr > th {
  background-color: #372235;
  color: white;
}
</style>

<template>
    <div class="input-group mb-3">
        <form type="files" method="POST" :data-id="dataId" :action="action" enctype="multipart/form-data">
            <label :for="id">
                <div data-id="dropZone">
                    <span>Для загрузки, перетащите файл сюда.</span>
                    <input type="file" :id="id" style="display: none" name="file" multiple>
                </div>
            </label>
        </form>
    </div>
</template>

<script>
    export default {
        data: function (id, action, dataId) {
            return {
                id,
                action,
                dataId
            }
        }
    }
</script>
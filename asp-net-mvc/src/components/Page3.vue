<template>
    <div>
        <h1 v-text="header"></h1>
        <div class="row">
            <div class="col-lg-8">
                <products-table :students="students" :loading="loading"></products-table>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from "axios";
    import ProductsTable from "./ProductsTable.vue";

    export default {
        components: {
            ProductsTable
        },
        data() {
            return {
                header: "Products",
                loading: false,
                students: []
            };
        },
        created() {
            this.fetchData();
        },
        methods: {
            fetchData() {
                this.loading = true;
                
                axios.get("/api/students")
                    .then(response => {
                        this.students = response.data;
                        this.loading = false;
                    })
                    .catch(error => {
                        this.loading = false;
                        console.log(error);
                    });
            }
        }
    }
</script>

<style lang="scss" scoped>
    $cyan: #22b8cf;

    h1 {
        color: $cyan;
    }
</style>
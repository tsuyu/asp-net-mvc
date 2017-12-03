<template>
    <table class="table table-bordered">
        <thead>
            <tr>
                <th>Id</th>
                <th style="width:30%">Name</th>
            </tr>
        </thead>
        <tbody>
            <tr v-if="loading">
                <td colspan="5" class="loading">Loading...</td>
            </tr>
            <tr v-for="student in students" key="student.Id">
                <td v-text="student.Id"></td>
                <td v-text="student.Name"></td>
            </tr>
        </tbody>
    </table>
</template>

<script>
    import moment from "moment";
    import numeral from "numeral";
    
    export default {
        props: ["loading", "students"]
    }

</script>

<style lang="scss" scoped>
    .low-inventory {
        background-color: #fff9db;
        color: #d9480f;
    }
    .loading {
        color: #2f9e44;
    }
</style>
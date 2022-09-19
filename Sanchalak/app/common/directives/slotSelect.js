app.directive("slotSelect", function () {
    //debugger;
    return {
        templateUrl: "UI/components/slotSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {


         /* $scope.getExamSlotMasterListGetActive();*/

        }],
        link: function (e, t, a) {
        }
    }
})
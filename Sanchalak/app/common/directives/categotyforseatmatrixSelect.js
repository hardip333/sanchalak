app.directive("categotyforseatmatrixSelect", function () {
    return {
        templateUrl: "UI/components/categotyforseatmatrixSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeGenReservationCategoryList = [];
            $scope.defaultCategoryList = [];
            var categoryObj = {
                Id: 0,
                ApplicationReservationName: "Select Category"
            }
            $scope.defaultCategoryList.push(categoryObj);
            $scope.getActiveGenReservationCategoryList = function () {
         
                $http({
                    method: 'POST',
                    url: 'api/GenReservationCategory/getGenReservationCategoryList_M',
                    data: {},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {
                            $scope.tempList = response.obj;
                            $scope.activeGenReservationCategoryList = $scope.defaultCategoryList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getActiveGenReservationCategoryList();


        }],
        link: function (e, t, a) {
        }
    }
})
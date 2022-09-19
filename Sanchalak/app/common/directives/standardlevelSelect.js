app.directive("standardlevelSelect", function () {
    return {
        templateUrl: "UI/components/standardlevelSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeAdmEligibleDegreeList = [];
            $scope.defaultEligibleDegreeList = [];
            var eligibleObj = {
                Id: 0,
                EligibleDegreeName: "Select Level"
            }
            $scope.defaultEligibleDegreeList.push(eligibleObj);

            $scope.getActiveAdmEligibleDegreeList = function () {

                $http({
                    method: 'POST',
                    url: 'api/AdmEligibleDegree/getAdmEligibleDegreeList',
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
                            $scope.activeAdmEligibleDegreeList = $scope.defaultEligibleDegreeList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getActiveAdmEligibleDegreeList();


        }],
        link: function (e, t, a) {
        }
    }
})
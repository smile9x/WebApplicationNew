(function () {
	var app = angular.module('myApp', []);

	app.controller('myCtrl', function ($http, $scope) {

		$http.get('api/me/users/all')
			.then(function (response) {
				$scope.users = response.data;
			}, function (response) {

			});

		$scope.deleteUser = function (email) {

			$http.get('api/me/users/delete/' + email)
				.then(function (response) {

				}, function (response) {

				});
		};
	});

})();
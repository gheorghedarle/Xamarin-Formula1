<img src="https://github.com/gheorghedarle/Xamarin-Formula1/blob/main/Screenshots/app_icon.png" width="96" />

# Xamarin Formula 1 App

![MIT License](https://img.shields.io/github/license/gheorghedarle/Xamarin-Formula1)

**Formula 1 App** is a simple app developed with **Xamarin** to display information about drivers, teams, circuits and seasons from 1950 to the current season. The app is developed in **Xamarin Shell** and **Ergast API**. In the Schedule screen, you can see the current season calendar and information about circuits and results for the finished races. In the Drivers and Teams screens, you can see current standings and information about them. In the History screen, you can select a season and see the schedule, driver and team standings for that season. The app is available in both **light** and **dark mode**.

If you like this repository you can support me on

<a href="https://www.buymeacoffee.com/gheorghedarle" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/guidelines/download-assets-sm-1.svg" alt="Buy Me A Coffee" width="175"></a>

## Screenshots

#### Light mode

<img src="https://github.com/gheorghedarle/Xamarin-Formula1/blob/main/Screenshots/light_mode.png?raw=true" Width="1620" />

#### Dark mode

<img src="https://github.com/gheorghedarle/Xamarin-Formula1/blob/main/Screenshots/dark_mode.png?raw=true" Width="1620" />

## Libraries

- [Xamarin Forms 5.0](https://github.com/xamarin/Xamarin.Forms)
- [Xamarin Essentials](https://github.com/xamarin/Essentials) 
- [Xamarin CommunityToolkit](https://github.com/xamarin/XamarinCommunityToolkit) (Popup, TabView, StateView, Converters)
- [Fody](https://github.com/Fody/Fody)
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)

## Setup

### Formula 1 Images API

**Formula 1 Images API** is an API developed in javascript to serve images for drivers, teams, countries and circuits.

To run the API:
- install the node modules using **npm-install.bat** or **npm install** command
- start the app using **npm-start.bat** or **npm start** command

### Formula 1 Information API

**Formula 1 Information API** is an API developed in javascript to scrape information about teams, drivers and circuits from [official F1 website](https://www.formula1.com/).

To run the API:
- install the node modules using **npm-install.bat** or **npm install** command
- start the app using **npm-start.bat** or **npm start** command

## Resources

The app is using data from [Ergast API](http://ergast.com/mrd/).

## Other versions

[MAUI-Formula1](https://github.com/gheorghedarle/MAUI-Formula1) - developed in **.net MAUI** (Work in Progress)

use actix_web::{web, App, HttpResponse, HttpServer};
use std::sync::Arc;
use serde::Serialize;

#[derive(Serialize, Clone)]
struct Bar {
    open: f64,
    high: f64,
    low: f64,
    close: f64,
}


#[derive(Serialize)]
struct BarResponse {
    symbol: String,
    bars: Vec<Bar>,
}

struct AppState {
    bar100: BarResponse,
    bar5000: BarResponse,
}

#[actix_web::main] // or #[tokio::main]
async fn main() -> std::io::Result<()> {
    let symbol: &str = "AAPL";

    let bar100 = BarResponse {
        symbol: symbol.to_string(),
        bars: vec![Bar {
            open: 432.3,
            high: 234.3,
            low: 324.3,
            close: 23.,
        }; 100],
    };

    let bar5000 = BarResponse {
        symbol: symbol.to_string(),
        bars: vec![Bar {
            open: 432.3,
            high: 234.3,
            low: 324.3,
            close: 23.,
        }; 5000],
    };

    let shared_state = Arc::new(AppState { bar100, bar5000 });

    HttpServer::new(move || {
        let shared_state = shared_state.clone(); // Clone the Arc for each closure

        App::new().app_data(web::Data::new(shared_state.clone())) // Store the shared state in app data
            .route("/bar/100", web::get().to(bar100_handler))
            .route("/bar/5000", web::get().to(bar5000_handler))
    })
        .bind(("localhost", 3006))?
        .run()
        .await
}

async fn bar100_handler(data: web::Data<Arc<AppState>>) -> HttpResponse {
    let state = data.get_ref(); // Get reference to shared state
    HttpResponse::Ok().json(&state.bar100)
}

async fn bar5000_handler(data: web::Data<Arc<AppState>>) -> HttpResponse {
    let state = data.get_ref(); // Get reference to shared state
    HttpResponse::Ok().json(&state.bar5000)
}
